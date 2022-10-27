using AE.EventManager;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AE.CharacterStats.UI
{
    public class StatsDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] TextMeshProUGUI labelText;
        [SerializeField] TextMeshProUGUI valueText;

        [NonSerialized] public CharacterStat Stat;

        public float Value
        {
            set {
                string percent = Stat.IsPercentual ? "%" : "";
                valueText.text = $"{value}{percent}";
            }
        }
        public string Label
        {
            set { labelText.text = value; }
        }

        private void OnValidate()
        {
            TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();

            labelText = texts[0];
            valueText = texts[1];
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EventManager.EventManager.TriggerStatEnter(Stat);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            EventManager.EventManager.TriggerStatExit();
        }
    }
}
