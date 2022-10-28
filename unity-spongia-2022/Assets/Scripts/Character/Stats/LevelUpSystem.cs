using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AE.CharacterStats
{
    public enum LevelUpModType
    {
        Damage,
        CritChance,
        Health,
        Resistance,
        DodgeChance,
        Stamina,
    }

    public class LevelUpSystem
    {
        public int Level { get; private set; }
        private int Exp;

        private int defaultExpRequirment = 80;
        private int incrementalExpRequirement = 100;

        public Dictionary<LevelUpModType, int> Levels;

        public int Damage
        {
            get { return Levels[LevelUpModType.Damage]; }
            private set { Levels[LevelUpModType.Damage] = value; }
        }
        public int CritChance
        {
            get { return Levels[LevelUpModType.CritChance]; }
            private set { Levels[LevelUpModType.CritChance] = value; }
        }
        public int Health
        {
            get { return Levels[LevelUpModType.Health]; }
            private set { Levels[LevelUpModType.Health] = value; }
        }
        public int Resistance
        {
            get { return Levels[LevelUpModType.Resistance]; }
            private set { Levels[LevelUpModType.Resistance] = value; }
        }
        public int DodgeChance
        {
            get { return Levels[LevelUpModType.DodgeChance]; }
            private set { Levels[LevelUpModType.DodgeChance] = value; }
        }
        public int Stamina
        {
            get { return Levels[LevelUpModType.Stamina]; }
            private set { Levels[LevelUpModType.Stamina] = value; }
        }

        private Dictionary<LevelUpModType, float> statBonuses = new Dictionary<LevelUpModType, float>()
        {
            {LevelUpModType.Damage, 3 },
            {LevelUpModType.CritChance, 2 },
            {LevelUpModType.Health, 20 },
            {LevelUpModType.Resistance, 5 },
            {LevelUpModType.DodgeChance, 5 },
            {LevelUpModType.Stamina, 10 },
        };

        private Character character;

        public LevelUpSystem(Character c = null, int? damageBonus = null, int? critChanceBonus = null,
            int? healthBonus = null, int? resistanceBonus = null, int? dodgeChanceBonus = null,
            int? staminaBonus = null)
        {
            character = c;

            Levels = new Dictionary<LevelUpModType, int> { };

            Damage = damageBonus is not null ? (int)damageBonus : 0;
            CritChance = critChanceBonus is not null ? (int)critChanceBonus : 0;
            Health = healthBonus is not null ? (int)healthBonus : 0;
            Resistance = resistanceBonus is not null ? (int)resistanceBonus : 0;
            DodgeChance = dodgeChanceBonus is not null ? (int)dodgeChanceBonus : 0;
            Stamina = staminaBonus is not null ? (int)staminaBonus : 0;

            UpdateMods();
        }

        public void addExp(int exp)
        {
            int remainingExp = exp;

            while (remainingExp - (defaultExpRequirment + Level * incrementalExpRequirement) >= 0)
            {
                remainingExp -= defaultExpRequirment + Level * incrementalExpRequirement;
                Level++;
            }

            Exp = remainingExp;
        }

        public bool LevelUp(LevelUpModType modType)
        {
            if (Levels.Values.Sum() < Level)
            {
                Levels[modType]++;
                UpdateMods();

                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateActiveCharacter(Character c)
        {
            character = c;
        }

        private void UpdateMods()
        {
            if (character is null)
                return;

            character.Damage.RemoveAllModifiersFromSource(this);
            character.CritChance.RemoveAllModifiersFromSource(this);
            character.HealthPoints.RemoveAllModifiersFromSource(this);
            character.DamageReduction.RemoveAllModifiersFromSource(this);
            character.DodgeChance.RemoveAllModifiersFromSource(this);
            character.Stamina.RemoveAllModifiersFromSource(this);

            if (Damage != 0)
                character.Damage.AddModifier(new StatModifier(Damage, StatModType.Flat, this));
            if (CritChance != 0)
                character.CritChance.AddModifier(new StatModifier(CritChance, StatModType.Flat, this));
            if (Health != 0)
                character.HealthPoints.AddModifier(new StatModifier(Health, StatModType.Flat, this));
            if (Resistance != 0)
                character.DamageReduction.AddModifier(new StatModifier(Resistance, StatModType.InverseProp, this));
            if (DodgeChance != 0)
                character.DodgeChance.AddModifier(new StatModifier(DodgeChance, StatModType.InverseProp, this));
            if (Stamina != 0)
                character.Stamina.AddModifier(new StatModifier(Stamina, StatModType.Flat, this));
        }
    }
}
