using AE.GameSave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static AbilityStorage;

namespace AE.Abilities.UI
{
    public class AbilitySlotPanel : MonoBehaviour
    {
        [SerializeField] Transform abilitySlotsGrid;
        [SerializeField] TextMeshProUGUI currentPageText;
        [SerializeField] AbilitySlot[] abilitySlots;
        [Space]
        [SerializeField] Character c = SaveData.PlayerCharacter;

        public event Action<AbilitySlot> OnAbilityClickedEvent;

        private int inventoryPagesCount
        {
            get
            {
                int abilityCount = c.UnlockedAbilities.Count;

                if (abilityCount <= abilitySlots.Length)
                    return 0;
                else
                    return (int)Math.Floor((float)(abilityCount / abilitySlots.Length)) - ((abilityCount % abilitySlots.Length == 0) ? 1 : 0);
            }
        }
        private int currentPage = 0;

        private void OnEnable()
        {
            for (int i = 0; i < abilitySlots.Length; i++)
            {
                abilitySlots[i].OnAbilityClickedEvent += handleClick;
            }
        }
        private void OnDisable()
        {
            for (int i = 0; i < abilitySlots.Length; i++)
            {
                abilitySlots[i].OnAbilityClickedEvent -= handleClick;
            }
        }

        private void handleClick(AbilitySlot abilitySlot)
        {
            OnAbilityClickedEvent?.Invoke(abilitySlot);
        }

        public void refreshUI()
        {
            if (currentPage > inventoryPagesCount)
                currentPage = inventoryPagesCount;

            currentPageText.text = $"{currentPage + 1} / {inventoryPagesCount + 1}";

            int i = 0;
            int skipped = 0;
            AbilityName[] abilities = c.UnlockedAbilities.ToArray();
            for (; i + skipped < abilities.Length - abilitySlots.Length * currentPage && i < abilitySlots.Length; i++)
                for (; i + skipped < abilities.Length - abilitySlots.Length * currentPage; skipped++)
                    if (!c.EquippedAbilities.Contains(abilities[i + skipped]))
                    {
                        abilitySlots[i].AbilityName = abilities[abilitySlots.Length * currentPage + i + skipped];
                        break;
                    }

            for (; i < abilitySlots.Length; i++)
            {
                abilitySlots[i].AbilityName = AbilityName.None;
            }
        }

        public void NextPage()
        {
            if (inventoryPagesCount == 0)
            {
                if (currentPage == 0)
                    return;
                currentPage = 0;
            }
            else if (currentPage < inventoryPagesCount)
                currentPage += 1;
            else
                currentPage = 0;

            refreshUI();
        }
        public void PreviousPage()
        {
            if (inventoryPagesCount == 0)
            {
                if (currentPage == 0)
                    return;
                currentPage = 0;
            }
            else if (currentPage > 0)
                currentPage -= 1;
            else
                currentPage = inventoryPagesCount;

            refreshUI();
        }

        private void OnValidate()
        {
            if (abilitySlotsGrid != null)
                abilitySlots = abilitySlotsGrid.GetComponentsInChildren<AbilitySlot>();
        }
    }
}
