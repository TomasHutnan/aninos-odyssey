using static AbilityStorage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Abilities;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace AE.Abilities.UI
{
    public class AbilityDisplay : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] AbilitySlot abilitySlot;
        [SerializeField] TextMeshProUGUI nameLabel;
        [SerializeField] TextMeshProUGUI descriptionLabel;

        [SerializeField] GameObject opacity;

        public event Action<AbilityDisplay> OnAbilityClickedEvent;

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

                abilitySlot.AbilityName = _abilityName;

                if (_abilityName == AbilityName.None)
                {
                    nameLabel.text = "EMPTY";
                    descriptionLabel.text = "";
                }
                else
                {
                    Ability ability = AbilityStorage.GetAbility[_abilityName];
                    nameLabel.text = ability.name;
                    descriptionLabel.text = ability.AbilityDescription;
                }
            }
        }

        private void OnEnable()
        {
            abilitySlot.OnAbilityClickedEvent += abilityClicked;
        }
        private void OnDisable()
        {
            abilitySlot.OnAbilityClickedEvent -= abilityClicked;
        }

        private void resloveSelection()
        {
            opacity.GetComponent<Outline>().enabled = IsSelected;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData != null && AbilityName != AbilityName.None)
            {
                if (eventData.button == PointerEventData.InputButton.Left
                    || eventData.button == PointerEventData.InputButton.Right)
                {
                    OnAbilityClickedEvent?.Invoke(this);
                }
            }
        }

        private void abilityClicked(AbilitySlot abilitySlot)
        {
            OnAbilityClickedEvent?.Invoke(this);
        }
    }
}
