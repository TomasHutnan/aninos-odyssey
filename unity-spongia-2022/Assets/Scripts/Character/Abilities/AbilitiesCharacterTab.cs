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

        private int emptyEquippedSlots { get { return equippedAbilities.MaxEquipped - c.EquippedAbilities.Count; } }

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
            if (emptyEquippedSlots > 0)
            {
                c.EquippedAbilities.Add(abilitySlot.AbilityName);
                deselect();
            }
            else if (selectedAbilitySlot == null && selectedAbilityDisplay == null)
            {
                selectedAbilitySlot = abilitySlot;
                abilitySlot.IsSelected = true;
            }
            else if (selectedAbilityDisplay != null)
            {
                c.EquippedAbilities.Remove(selectedAbilityDisplay.AbilityName);
                c.EquippedAbilities.Add(abilitySlot.AbilityName);

                deselect();
                refreshUI();
            }
            else if (selectedAbilitySlot == abilitySlot)
            {
                deselect();
                refreshUI();
            }
            else
            {
                deselect();
                selectedAbilitySlot = abilitySlot;
                abilitySlot.IsSelected = true;
            }
            refreshUI();
        }
        private void handleEquippedClick(AbilityDisplay abilityDisplay)
        {
            if (selectedAbilitySlot == null && selectedAbilityDisplay == null)
            {
                deselect();
                selectedAbilityDisplay = abilityDisplay;
                abilityDisplay.IsSelected = true;
            }
            else if (selectedAbilitySlot != null)
            {
                c.EquippedAbilities.Remove(abilityDisplay.AbilityName);
                c.EquippedAbilities.Add(selectedAbilitySlot.AbilityName);

                deselect();
                refreshUI();
            }
            else if (selectedAbilityDisplay == abilityDisplay)
            {
                deselect();
                refreshUI();
            }
            else
            {
                deselect();
                selectedAbilityDisplay = abilityDisplay;
                abilityDisplay.IsSelected = true;
            }
            refreshUI();
        }

        private void deselect()
        {
            if (selectedAbilitySlot != null)
            {
                selectedAbilitySlot.IsSelected = false;
                selectedAbilitySlot = null;
            }
            if (selectedAbilityDisplay != null)
            {
                selectedAbilityDisplay.IsSelected = false;
                selectedAbilityDisplay = null;
            }
        }

        private void refreshUI()
        {
            ownedAbilities.refreshUI();
            equippedAbilities.refreshUI();
        }
    }
}
