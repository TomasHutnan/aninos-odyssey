using AE.GameSave;
using AE.SceneManagment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AE.GameSave.NetworkingController;

public class UploadDialogManager : MonoBehaviour
{
    [SerializeField] NetworkingController networkingController;

    [SerializeField] Transform saveButtonsHolder;
    [SerializeField] GameObject[] buttons;




    private void Start()
    {
        for (int i = 1; i < buttons.Length; i++)
        {
            if (SaveController.IsSlotOccupied((SaveSlot)i))
                buttons[i].SetActive(true);
            else
                buttons[i].SetActive(false);
        }
    }

    public void HandleButtonPress(int saveSlotIndex)
    {
        SaveSlot saveSlot = (SaveSlot)saveSlotIndex;
        
        if (SaveController.IsSlotOccupied(saveSlot))
        {
            gameObject.SetActive(false);
            networkingController.Upload(saveSlot);
        }
        else
        {
            print("Trying to upload empty slot!");
            return;
        }
    }

    private void OnValidate()
    {
        if (saveButtonsHolder != null)
        {
            buttons = new GameObject[saveButtonsHolder.childCount];

            for (int i = 0; i < saveButtonsHolder.childCount; i++)
            {
                buttons[i] = saveButtonsHolder.GetChild(i).gameObject;
            }
        }
    }
}
