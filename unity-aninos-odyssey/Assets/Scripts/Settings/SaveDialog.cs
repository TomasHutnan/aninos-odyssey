using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDialog : MonoBehaviour
{
    [SerializeField] GameObject saveSlotsHolder;
    [SerializeField] GameObject overwriteDialog;

    private SaveSlot selectedSlot;

    private void OnEnable()
    {
        saveSlotsHolder.SetActive(true);
        overwriteDialog.SetActive(false);

        selectedSlot = SaveSlot.None;
    }

    public void HandleSaveSlotSelection(int saveSlotIndex)
    {
        selectedSlot = (SaveSlot)saveSlotIndex;
        if (SaveController.IsSlotOccupied(selectedSlot))
        {
            saveSlotsHolder.SetActive(false);
            overwriteDialog.SetActive(true);
        }
        else
        {
            SaveData.Save(selectedSlot);
            gameObject.SetActive(false);
        }
    }

    public void ApproveOverwrite()
    {
        if (selectedSlot != SaveSlot.None)
        {
            SaveData.Save(selectedSlot);
            gameObject.SetActive(false);
        }
        else
        {
            print("Attempting to save to SaveSlot.None!");
        }
    }

    public void RejectOverwrite()
    {
        selectedSlot = SaveSlot.None;
        saveSlotsHolder.SetActive(true);
        overwriteDialog.SetActive(false);
    }
}
