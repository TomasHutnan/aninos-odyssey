using AE.EventManager;
using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AE.Items.UI
{
    public enum PromptType
    {
        None,
        Sell,
        Buy,
    }
    public class ItemPrompt : MonoBehaviour
    {
        [SerializeField] ItemSlot ItemSlot;
        [SerializeField] GameObject ItemPromptGameObject;
        [Space]
        [SerializeField] TextMeshProUGUI HeaderText;
        [SerializeField] TextMeshProUGUI QuestionText;
        [SerializeField] GameObject AskAgainCheckbox;

        private PromptType promptType = PromptType.None;

        private Item item;

        bool? dontAskAgain = null;

        private void OnEnable()
        {
            EventManager.EventManager.ItemPromptQuestionEvent += handleQuestion;
        }
        private void OnDisable()
        {
            EventManager.EventManager.ItemPromptQuestionEvent -= handleQuestion;
        }

        private void handleQuestion(Item _item, PromptType _promptType)
        {
            if (promptType != PromptType.None || _promptType == PromptType.None)
                return;

            item = _item;
            promptType = _promptType;
            ItemSlot.Item = item;

            HeaderText.text = "Confirm item " + (promptType == PromptType.Buy ? "Purchase" : "Sale");
            QuestionText.text = "Are you sure you want to <color=#FFD700>" + (promptType == PromptType.Buy ? "buy" : "sell") + "</color> the following item?";

            AskAgainCheckbox.SetActive(promptType == PromptType.Sell);

            ItemPromptGameObject.SetActive(true);
        }
        private void handleAnswer(bool answer)
        {
            EventManager.EventManager.TriggerItemPromptAnswer(item, promptType, answer);

            promptType = PromptType.None;
            item = null;
            ItemPromptGameObject.SetActive(false);
        }

        public void ToggleDontAsk(bool value)
        {
            dontAskAgain = value;
        }
        public void Yes()
        {
            if (dontAskAgain == true && promptType == PromptType.Sell)
                SaveData.ConfirmSell = false;

            handleAnswer(true);
        }
        public void No()
        {
            handleAnswer(false);
        }
    }
}
