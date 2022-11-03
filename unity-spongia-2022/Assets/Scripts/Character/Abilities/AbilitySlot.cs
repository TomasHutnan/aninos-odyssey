using Abilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using static AbilityStorage;

namespace AE.Abilities.UI
{
    public class AbilitySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image background;
        [SerializeField] Image icon;
        [SerializeField] Image foreground;

        public event Action<AbilitySlot> OnAbilityClickedEvent;

        private bool _isSelected = false;
        public bool IsSelected 
        { 
            get { return _isSelected; } 
            set 
            { 
                _isSelected = value;
                resloveSelection();
            } 
        }

        private AbilityName _abilityName;
        public AbilityName AbilityName
        {
            get { return _abilityName; }
            set
            {
                _abilityName = value;

                if (_abilityName == AbilityName.None)
                {
                    background.enabled = false;
                    icon.enabled = false;
                    foreground.enabled = false;

                    IsSelected = false;
                }
                else
                {
                    Ability ability = GetAbility[_abilityName];

                    background.sprite = null; // ability.icon_bg
                    background.enabled = true;

                    icon.sprite = null; // ability.icon
                    icon.enabled = true;

                    foreground.sprite = null; // ability.icon_fg
                    foreground.enabled = true;
                }
            }
        }

        private void resloveSelection()
        {
            background.GetComponent<Outline>().enabled = IsSelected;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData != null && _abilityName != AbilityName.None)
            {
                if (eventData.button == PointerEventData.InputButton.Left
                    || eventData.button == PointerEventData.InputButton.Right)
                {
                    OnAbilityClickedEvent?.Invoke(this);
                    EventManager.EventManager.TriggerAbilitySlotExit();
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_abilityName != AbilityName.None)
                EventManager.EventManager.TriggerAbilitySlotEnter(_abilityName);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            EventManager.EventManager.TriggerAbilitySlotExit();
        }
    }
}
