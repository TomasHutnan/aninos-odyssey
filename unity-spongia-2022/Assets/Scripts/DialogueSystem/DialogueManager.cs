using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AE.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] Dialogue dialogue;

        [SerializeField] TextMeshProUGUI nameComponent;
        [SerializeField] TextMeshProUGUI textComponent;

        public float speed = 0.05f;

        private int index;

        public event Action OnDialogueFinishEvent;

        public bool IsTyping { get { return textComponent.text != dialogue.sentances[index].text; } }

        private void Start()
        {
            textComponent.text = string.Empty;

            startDialogue();
        }

        private void startDialogue()
        {
            index = 0;

            StartCoroutine(typeLine());
        }

        private IEnumerator typeLine()
        {
            nameComponent.text = dialogue.sentances[index].name.ToString();
            foreach (char c in dialogue.sentances[index].text.ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(speed);
            }
        }

        public void NewLine()
        {
            if (index < dialogue.sentances.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(typeLine());
            }
            else
            {
                OnDialogueFinishEvent?.Invoke();
            }
        }
        public void ShowFullLine()
        {
            if (index < dialogue.sentances.Length)
            {
                StopAllCoroutines();
                textComponent.text = dialogue.sentances[index].text;
            }
        }
    }
}
