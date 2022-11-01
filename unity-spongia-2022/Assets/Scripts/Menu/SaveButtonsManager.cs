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
        [SerializeField] GameObject[] buttons;
        [SerializeField] TextMeshProUGUI autoSaveText;

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
            buttons = new GameObject[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                buttons[i] = transform.GetChild(i).gameObject;
            }

            autoSaveText = buttons[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        public void HandleButtonPress(int saveSlotIndex)
        {
            if (saveSlotIndex == -1)
            {
                SaveController.StartNew();
            }
            else if (SaveController.IsSlotOccupied((SaveSlot)saveSlotIndex))
            {
                SaveController.ActivateSave((SaveSlot)saveSlotIndex);
            }
            else
            {
                print("Trying to load empty slot!");
                return;
            }

            SceneUtils.LoadScene("GameScene");
        }
    }
}
