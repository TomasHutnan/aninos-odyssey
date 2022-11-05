using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using AE.GameSave;
using AE.SceneManagment;

namespace AE.MainMenu
{
    public class SaveButtonsManager : MonoBehaviour
    {
        [SerializeField] Transform saveButtonsHolder;
        [SerializeField] GameObject[] buttons;

        [SerializeField] GameObject overwriteDialog;

        private SaveSlot selectedSlot = SaveSlot.None;

        private void Start()
        {
            for (int i = 1; i < buttons.Length; i++)
            {
                if (SaveController.IsSlotOccupied((SaveSlot)i-1))
                    buttons[i].SetActive(true);
                else
                    buttons[i].SetActive(false);
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

        public void HandleButtonPress(int saveSlotIndex)
        {
            SaveSlot saveSlot = (SaveSlot)saveSlotIndex;
            if (saveSlotIndex == -1)
            {
                if (SaveController.IsSlotOccupied(SaveSlot.AutoSave))
                {
                    selectedSlot = SaveSlot.AutoSave;
                    overwriteDialog.SetActive(true);
                }
                else
                {
                    newGame();
                }
            }
            else if (SaveController.IsSlotOccupied(saveSlot))
            {
                SaveController.ActivateSave(saveSlot);
                SceneUtils.LoadScene("GameScene");
            }
            else
            {
                print("Trying to load empty slot!");
                return;
            }
        }

        private void newGame()
        {
            SaveController.StartNew();
            SaveData.AutoSave();
            SceneUtils.LoadScene("IntroScene");
        } 

        public void ApproveOverwrite()
        {
            if (selectedSlot != SaveSlot.None)
            {
                newGame();
            }
            else
            {
                print("Attempting to save to SaveSlot.None!");
            }
        }

        public void RejectOverwrite()
        {
            selectedSlot = SaveSlot.None;
            overwriteDialog.SetActive(false);
        }
    }
}
