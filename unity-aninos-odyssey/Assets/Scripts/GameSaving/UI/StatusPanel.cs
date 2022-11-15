using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using AE.GameSave;
using AE.SceneManagment;
using System;
using System.Linq;

public class StatusPanel : MonoBehaviour
{
    [SerializeField] GameObject holder;
    [SerializeField] TextMeshProUGUI statusLabel;
    [SerializeField] TextMeshProUGUI statusDescription;
    [SerializeField] GameObject keyHodler;
    [SerializeField] TextMeshProUGUI keyText;

    private void OnEnable()
    {
        NetworkingController.OnDownloadStatusUpdate += HandleDownloadStatusUpdate;
        NetworkingController.OnUploadStatusUpdate += HandleUploadStatusUpdate;
    }
    private void OnDisable()
    {
        NetworkingController.OnDownloadStatusUpdate -= HandleDownloadStatusUpdate;
        NetworkingController.OnUploadStatusUpdate -= HandleUploadStatusUpdate;
    }

    private void HandleDownloadStatusUpdate(DownloadNetworkStatus value)
    {
        holder.SetActive(true);
        SetNone();
        if (value == DownloadNetworkStatus.None)
        {
            holder.SetActive(false);
            return;
        }
        statusLabel.text = value.ToString();
        if (value == DownloadNetworkStatus.Success)
        {
            SceneUtils.LoadScene("GameScene");
        }
        else if (value == DownloadNetworkStatus.ServerError || value == DownloadNetworkStatus.NetworkError || value == DownloadNetworkStatus.NotFound)
        {
            statusDescription.text = "An error occured, please check your connection.\nService may be down.";
        }
    }

    private void HandleUploadStatusUpdate(UploadNetworkStatus value)
    {
        holder.SetActive(true);
        SetNone();
        if (value == UploadNetworkStatus.None)
        {
            holder.SetActive(false);
            return;
        }

        statusLabel.text = value.ToString();
        if (value == UploadNetworkStatus.Success)
        {
            statusDescription.text = "Copy your save key below.";
            keyHodler.SetActive(true);
            keyText.text = removeNonIntegers(NetworkingController.UploadKey);
        }
        else if (value == UploadNetworkStatus.ServerError || value == UploadNetworkStatus.NetworkError)
        {
            statusDescription.text = "An error occured, please check your connection.\nService may be down.";
        }
    }

    private string removeNonIntegers(string s)
    {
        char[] integers = "0123456789".ToCharArray();

        string intOnly = string.Empty;

        foreach (char c in s.ToCharArray())
        {
            if (integers.Contains(c))
            {
                intOnly += c;
            }
        }

        return intOnly;
    }

    public void CopyKey()
    {
        GUIUtility.systemCopyBuffer = removeNonIntegers(NetworkingController.UploadKey);
    }

    public void Close()
    {
        SetNone();
        holder.SetActive(false);
    }

    private void SetNone()
    {
        statusLabel.text = string.Empty;
        statusDescription.text = string.Empty;
        keyHodler.SetActive(false);
        keyText.text = string.Empty;
    }
}
