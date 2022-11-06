using AE.Abilities.UI;
using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static AbilityStorage;

namespace AE.Fight.UI
{
    public class AbilitySlotsPopulator : MonoBehaviour
    {
        [SerializeField] AbilitySlot[] abilitySlots;

        Character c = SaveData.PlayerCharacter;

        private void Start()
        {
            EnableButtons();
        }

        private void OnEnable()
        {
            AbilityName[] equipped = c.EquippedAbilities.ToArray();

            for (int i = 0; i < equipped.Length && i < abilitySlots.Length; i++)
            {
                abilitySlots[i].OnAbilityClickedEvent += DisableButton;
            }

        }
        private void OnDisable()
        {
            AbilityName[] equipped = c.EquippedAbilities.ToArray();

            for (int i = 0; i < equipped.Length && i < abilitySlots.Length; i++)
            {
                abilitySlots[i].OnAbilityClickedEvent -= DisableButton;
            }

        }

        private void OnValidate()
        {
            abilitySlots = GetComponentsInChildren<AbilitySlot>();
        }

        private void DisableButton(AbilitySlot abilitySlot)
        {
            abilitySlot.AbilityName = AbilityName.None;
        }

        public void DisableButtons()
        {
            GetComponent<Image>().enabled = false;

            foreach (AbilitySlot abilitySlot in abilitySlots)
            {
                abilitySlot.AbilityName = AbilityName.None;
            }
        }
        public void EnableButtons()
        {
            GetComponent<Image>().enabled = true;

            AbilityName[] equipped = c.EquippedAbilities.ToArray();

            int i = 0;
            for (; i < equipped.Length && i < abilitySlots.Length; i++)
            {
                abilitySlots[i].AbilityName = equipped[i];
            }
            for (; i < abilitySlots.Length; i++)
            {
                abilitySlots[i].AbilityName = AbilityName.None;
            }
        }
    }
}
