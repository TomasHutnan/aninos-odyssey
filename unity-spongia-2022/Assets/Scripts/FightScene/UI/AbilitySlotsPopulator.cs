using AE.Abilities.UI;
using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AbilityStorage;

namespace AE.Fight.UI
{
    public class AbilitySlotsPopulator : MonoBehaviour
    {
        [SerializeField] AbilitySlot[] abilitySlots;

        Character c = SaveData.PlayerCharacter;

        private void Start()
        {
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

        private void OnValidate()
        {
            abilitySlots = GetComponentsInChildren<AbilitySlot>();
        }
    }
}
