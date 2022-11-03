using AE.GameSave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AbilityStorage;

namespace AE.Abilities.UI
{
    public class AbilityDisplayPanel : MonoBehaviour
    {
        [SerializeField] Transform abilityDisplaysGrid;
        [SerializeField] AbilityDisplay[] abilityDisplays;
        [Space]
        [SerializeField] Character c = SaveData.PlayerCharacter;

        public int MaxEquipped { get { return abilityDisplays.Length; } }

        public event Action<AbilityDisplay> OnAbilityClickedEvent;

        private void OnEnable()
        {
            for (int i = 0; i < abilityDisplays.Length; i++)
            {
                abilityDisplays[i].OnAbilityClickedEvent += handleClick;
            }
        }
        private void OnDisable()
        {
            for (int i = 0; i < abilityDisplays.Length; i++)
            {
                abilityDisplays[i].OnAbilityClickedEvent -= handleClick;
            }
        }

        private void handleClick(AbilityDisplay abilityDisplay)
        {
            OnAbilityClickedEvent?.Invoke(abilityDisplay);
        }

        public void refreshUI()
        {
            int i = 0;
            AbilityName[] abilities = c.EquippedAbilities.ToArray();
            for (; i < abilities.Length && i < abilityDisplays.Length; i++)
            {
                abilityDisplays[i].AbilityName = abilities[i];
            }
            for (; i < abilityDisplays.Length; i++)
            {
                abilityDisplays[i].AbilityName = AbilityName.None;
            }
        }

        private void OnValidate()
        {
            if (abilityDisplaysGrid != null)
                abilityDisplays = abilityDisplaysGrid.GetComponentsInChildren<AbilityDisplay>();
        }
    }
}
