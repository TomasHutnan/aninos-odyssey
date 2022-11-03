using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AbilityStorage;

namespace AE.Abilities.UI
{
    public class AbilitiesCharacterTab : MonoBehaviour
    {
        [SerializeField] AbilitySlotPanel ownedAbilities;
        [SerializeField] AbilityDisplayPanel equippedAbilities;

        AbilitySlot selectedAbilitySlot = null;
        AbilityDisplay selectedAbilityDisplay = null;

        Character c = SaveData.PlayerCharacter;


        private void OnEnable()
        {
            refreshUI();

            ownedAbilities.OnAbilityClickedEvent += handleOwnedClick;
            equippedAbilities.OnAbilityClickedEvent += handleEquippedClick;
        }
        private void OnDisable()
        {
            ownedAbilities.OnAbilityClickedEvent += handleOwnedClick;
            equippedAbilities.OnAbilityClickedEvent += handleEquippedClick;
        }

        private void handleOwnedClick(AbilitySlot abilitySlot)
        {
            if (selectedAbilitySlot == null && selectedAbilityDisplay == null)
            {
                selectedAbilitySlot = abilitySlot;
                abilitySlot.IsSelected = true;
            }
            else if (selectedAbilityDisplay != null)
            {
                c.EquippedAbilities.Remove(selectedAbilityDisplay.AbilityName);
                c.EquippedAbilities.Add(abilitySlot.AbilityName);

                selectedAbilityDisplay.IsSelected = false;
                selectedAbilityDisplay = null;

                refreshUI();
            }
            else
            {
                selectedAbilitySlot.IsSelected = false;

                selectedAbilitySlot = abilitySlot;
                abilitySlot.IsSelected = true;
            }
        }
        private void handleEquippedClick(AbilityDisplay abilityDisplay)
        {
            if (selectedAbilitySlot == null && selectedAbilityDisplay == null)
            {
                selectedAbilityDisplay = abilityDisplay;
                abilityDisplay.IsSelected = true;
            }
            else if (selectedAbilitySlot != null)
            {
                c.EquippedAbilities.Remove(abilityDisplay.AbilityName);
                c.EquippedAbilities.Add(selectedAbilitySlot.AbilityName);

                selectedAbilitySlot.IsSelected = false;
                selectedAbilitySlot = null;

                refreshUI();
            }
            else
            {
                selectedAbilityDisplay.IsSelected = false;

                selectedAbilityDisplay = abilityDisplay;
                abilityDisplay.IsSelected = true;
            }
        }

        private void refreshUI()
        {
            ownedAbilities.refreshUI();
            equippedAbilities.refreshUI();
        }
    }
}
