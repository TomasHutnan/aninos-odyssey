using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace AE.GameSave {
    public class NetworkingController : MonoBehaviour
    {

        public static DownloadNetworkStatus DownloadStatus = DownloadNetworkStatus.Success;
        public static UploadNetworkStatus UploadStatus = UploadNetworkStatus.Success;
        public static string Key = null;

        // Start is called before the first frame update
        void Start()
        {
            DownloadStatus = DownloadNetworkStatus.Success;
            UploadStatus = UploadNetworkStatus.Success;
            Key = null;
        }

        public void Download(int key) {
            DownloadStatus = DownloadNetworkStatus.Pending;
            StartCoroutine(SendDownloadRequest(key));
        }

        IEnumerator SendDownloadRequest(int key) {
            using (UnityWebRequest webRequest = UnityWebRequest.Get("https://aninos-odyssey-server.vercel.app/api/download?key=" + key)) {
                yield return webRequest.SendWebRequest();
                if (webRequest.result != UnityWebRequest.Result.Success) {
                    DownloadStatus = DownloadNetworkStatus.NetworkError;
                } else {
                    string received = webRequest.downloadHandler.text;
                    if (received.StartsWith("{\"message\":") || received == "{\"data\":null}") {
                        DownloadStatus = DownloadNetworkStatus.ServerError;
                    } else if (received == "{\"data\":\"\"}") {
                        DownloadStatus = DownloadNetworkStatus.NotFound;
                    } else {
                        DownloadStatus = DownloadNetworkStatus.Success;
                        SaveData.Load(SaveSlot.None, JsonConvert.DeserializeObject<JSONSave>(Encoding.UTF8.GetString(Convert.FromBase64String(received.Substring(9, received.Length - 11)))));
                    }
                }
            }
        }

        public void Upload(SaveSlot slot) {
            if (!SaveController.IsSlotOccupied(slot))
                return;

            UploadStatus = UploadNetworkStatus.Pending;
            Key = null;
            StartCoroutine(SendUploadRequest(Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(SaveController.GetData(slot), Formatting.Indented, new JsonSerializerSettings{ NullValueHandling = NullValueHandling.Include })))));
        }

        IEnumerator SendUploadRequest(string data) {
            WWWForm form = new WWWForm();
            form.AddField("data", data);
            using (UnityWebRequest webRequest = UnityWebRequest.Post("https://aninos-odyssey-server.vercel.app/api/upload", form)) {
                yield return webRequest.SendWebRequest();
                if (webRequest.result != UnityWebRequest.Result.Success) {
                    UploadStatus = UploadNetworkStatus.NetworkError;
                } else {
                    string received = webRequest.downloadHandler.text;
                    if (received.StartsWith("{\"message\":") || received == "{\"key\":null}") {
                        UploadStatus = UploadNetworkStatus.ServerError;
                    } else {
                        DownloadStatus = DownloadNetworkStatus.Success;
                        Key = received.Substring(8, received.Length - 10);
                    }
                }
            }
        }

        public enum DownloadNetworkStatus {
            Pending, Success, NetworkError, ServerError, NotFound
        }

        public enum UploadNetworkStatus {
            Pending, Success, NetworkError, ServerError
        }
    }
}