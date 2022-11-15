using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.CharacterStats.UI
{
    public class StatsPanel : MonoBehaviour
    {
        [SerializeField] Character c;
        [SerializeField] StatsDisplay[] statsDisplays;

        private void OnEnable()
        {
            if (c is null)
                c = SaveData.PlayerCharacter;

            c.EquipmentUpdateEvent += updateStatValues;

            setStats();
            updateStatNames();
            updateStatValues();
        }

        private void OnDisable()
        {
            c.EquipmentUpdateEvent -= updateStatValues;
        }

        private void setStats()
        {
            statsDisplays[0].Stat = c.Damage;
            statsDisplays[1].Stat = c.CritChance;
            statsDisplays[2].Stat = c.HealthPoints;
            statsDisplays[3].Stat = c.DamageReduction;
            statsDisplays[4].Stat = c.DodgeChance;
            statsDisplays[5].Stat = c.Stamina;
            statsDisplays[6].Stat = c.Mana;
            statsDisplays[7].Stat = c.Weight;
        }

        private void updateStatNames()
        {
            statsDisplays[0].Label = c.Damage.Label;
            statsDisplays[1].Label = c.CritChance.Label;
            statsDisplays[2].Label = c.HealthPoints.Label;
            statsDisplays[3].Label = c.DamageReduction.Label;
            statsDisplays[4].Label = c.DodgeChance.Label;
            statsDisplays[5].Label = c.Stamina.Label;
            statsDisplays[6].Label = c.Mana.Label;
            statsDisplays[7].Label = c.Weight.Label;
        }

        private void updateStatValues()
        {
            statsDisplays[0].Value = c.Damage.Value;
            statsDisplays[1].Value = c.CritChance.Value;
            statsDisplays[2].Value = c.HealthPoints.Value;
            statsDisplays[3].Value = c.DamageReduction.Value;
            statsDisplays[4].Value = c.DodgeChance.Value;
            statsDisplays[5].Value = c.Stamina.Value;
            statsDisplays[6].Value = c.Mana.Value;
            statsDisplays[7].Value = c.Weight.Value;
        }

        private void OnValidate()
        {
            statsDisplays = GetComponentsInChildren<StatsDisplay>();
        }
    }
}
