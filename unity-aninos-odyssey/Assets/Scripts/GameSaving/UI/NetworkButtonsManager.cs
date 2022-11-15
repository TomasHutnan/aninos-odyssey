using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NetworkButtonsManager : MonoBehaviour
{
    [SerializeField] NetworkingController networkingController;
    [SerializeField] GameObject uploadButton;
    [SerializeField] TMP_InputField keyInputField;

    [SerializeField] GameObject downloadDialog;

    private void Start()
    {
        uploadButton.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            if (SaveController.IsSlotOccupied((SaveSlot)i))
            {
                uploadButton.SetActive(true);
                break;
            }
        }
    }

    public void Download()
    {
        string _input = keyInputField.text;
        if (_input == null || _input == "")
            return;

        int _key;
        bool isInteger = int.TryParse(_input, out _key);
        if (!isInteger)
            return;

        downloadDialog.SetActive(false);
        networkingController.Download(_key);
    }
}
