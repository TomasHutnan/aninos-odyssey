using static AbilityStorage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Abilities;
using UnityEngine.EventSystems;

namespace AE.Abilities.UI
{
    public class AbilityDisplay : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI nameLabel;
        [SerializeField] TextMeshProUGUI descriptionLabel;

        public event Action<AbilityName> OnAbilityClickedEvent;

        private AbilityName _abilityName;
        public AbilityName AbilityName
        {
            get { return _abilityName; }
            set
            {
                _abilityName = value;

                if (_abilityName == AbilityName.None)
                {
                    icon.sprite = null;
                    nameLabel.text = "Missing name";
                    descriptionLabel.text = "Missing description";
                }
                else
                {
                    Ability ability = AbilityStorage.GetAbility[_abilityName];
                    //icon.sprite = ability.Icon;
                    nameLabel.text = ability.name;
                    descriptionLabel.text = ability.AbilityDescription;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData != null && AbilityName != AbilityName.None)
            {
                if (eventData.button == PointerEventData.InputButton.Left
                    || eventData.button == PointerEventData.InputButton.Right)
                {
                    OnAbilityClickedEvent?.Invoke(AbilityName);
                }
            }
        }
    }
}
