using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace AE.GameSave
{
    public enum DownloadNetworkStatus
    {
        None, Pending, Success, NetworkError, ServerError, NotFound
    }

    public enum UploadNetworkStatus
    {
        None, Pending, Success, NetworkError, ServerError
    }
    public class NetworkingController : MonoBehaviour
    {
        public static event Action<DownloadNetworkStatus> OnDownloadStatusUpdate;
        public static event Action<UploadNetworkStatus> OnUploadStatusUpdate;

        public static string UploadKey = null;

        public void Download(int key) {
            OnDownloadStatusUpdate?.Invoke(DownloadNetworkStatus.Pending);
            StartCoroutine(SendDownloadRequest(key));
        }

        IEnumerator SendDownloadRequest(int key) {
            using (UnityWebRequest webRequest = UnityWebRequest.Get("https://aninos-odyssey-server.vercel.app/api/download?key=" + key)) {
                yield return webRequest.SendWebRequest();
                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    OnDownloadStatusUpdate?.Invoke(DownloadNetworkStatus.NetworkError);
                } 
                else 
                {
                    string received = webRequest.downloadHandler.text;
                    if (received.StartsWith("{\"message\":") || received == "{\"data\":null}") {
                        OnDownloadStatusUpdate?.Invoke(DownloadNetworkStatus.ServerError);
                    } else if (received == "{\"data\":\"\"}")
                    {
                        OnDownloadStatusUpdate?.Invoke(DownloadNetworkStatus.NotFound);
                    } else
                    {
                        OnDownloadStatusUpdate?.Invoke(DownloadNetworkStatus.Success);
                        SaveData.Load(SaveSlot.None, JsonConvert.DeserializeObject<JSONSave>(Encoding.UTF8.GetString(Convert.FromBase64String(received.Substring(9, received.Length - 11)))));
                    }
                }
            }
        }

        public void Upload(SaveSlot slot) {
            if (!SaveController.IsSlotOccupied(slot))
                return;

            OnUploadStatusUpdate?.Invoke(UploadNetworkStatus.Pending);
            UploadKey = null;
            StartCoroutine(SendUploadRequest(Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(SaveController.GetData(slot), Formatting.Indented, new JsonSerializerSettings{ NullValueHandling = NullValueHandling.Include })))));
        }

        IEnumerator SendUploadRequest(string data) {
            WWWForm form = new WWWForm();
            form.AddField("data", data);
            using (UnityWebRequest webRequest = UnityWebRequest.Post("https://aninos-odyssey-server.vercel.app/api/upload", form)) {
                yield return webRequest.SendWebRequest();
                print(webRequest.result);
                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    OnUploadStatusUpdate?.Invoke(UploadNetworkStatus.NetworkError);
                } else {
                    string received = webRequest.downloadHandler.text;
                    if (received.StartsWith("{\"message\":") || received == "{\"key\":null}")
                    {
                        OnUploadStatusUpdate?.Invoke(UploadNetworkStatus.ServerError);
                    } else
                    {
                        OnUploadStatusUpdate?.Invoke(UploadNetworkStatus.Success);
                        UploadKey = received.Substring(8, received.Length - 10);
                    }
                }
            }
        }
    }
}