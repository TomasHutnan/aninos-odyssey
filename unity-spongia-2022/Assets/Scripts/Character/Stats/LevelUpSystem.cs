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
        Mana,
    }

    public class LevelUpSystem
    {
        public int Level { get; private set; }
        public int Exp { get; private set; }

        public int UnspentSkillPoints { get { return Level - Levels.Values.Sum(); } }

        private int defaultExpRequirement = 80;
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
        public int Mana
        {
            get { return Levels[LevelUpModType.Mana]; }
            private set { Levels[LevelUpModType.Mana] = value; }
        }

        private Dictionary<LevelUpModType, float> statBonuses = new Dictionary<LevelUpModType, float>()
        {
            {LevelUpModType.Damage, 3 },
            {LevelUpModType.CritChance, 2 },
            {LevelUpModType.Health, 20 },
            {LevelUpModType.Resistance, 5 },
            {LevelUpModType.DodgeChance, 5 },
            {LevelUpModType.Stamina, 10 },
            {LevelUpModType.Mana, 10 },
        };

        private Character character;

        public LevelUpSystem(Character c = null, int level = 0, int exp = 0,
            int damageBonus = 0, int critChanceBonus = 0,
            int healthBonus = 0, int resistanceBonus = 0, int dodgeChanceBonus = 0,
            int staminaBonus = 0, int manaBonus = 0)
        {
            character = c;

            Level = level;
            Exp = 0;
            addExp(exp);

            Levels = new Dictionary<LevelUpModType, int> { };

            Damage = damageBonus;
            CritChance = critChanceBonus;
            Health = healthBonus;
            Resistance = resistanceBonus;
            DodgeChance = dodgeChanceBonus;
            Stamina = staminaBonus;
            Mana = manaBonus;

            if (character != null)
                UpdateMods();
        }

        public void addExp(int exp)
        {
            int remainingExp = Exp + exp;

            while (remainingExp - (defaultExpRequirement + Level * incrementalExpRequirement) >= 0)
            {
                remainingExp -= defaultExpRequirement + Level * incrementalExpRequirement;
                Level++;
            }

            Exp = remainingExp;
        }

        public bool LevelUp(LevelUpModType modType)
        {
            if (UnspentSkillPoints > 0)
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
            UpdateMods();
        }

        private void UpdateMods()
        {
            if (character == null)
                return;

            character.Damage.RemoveAllModifiersFromSource(this);
            character.CritChance.RemoveAllModifiersFromSource(this);
            character.HealthPoints.RemoveAllModifiersFromSource(this);
            character.DamageReduction.RemoveAllModifiersFromSource(this);
            character.DodgeChance.RemoveAllModifiersFromSource(this);
            character.Stamina.RemoveAllModifiersFromSource(this);
            character.Mana.RemoveAllModifiersFromSource(this);

            if (Damage != 0)
                character.Damage.AddModifier(new StatModifier(statBonuses[LevelUpModType.Damage] * Damage, StatModType.Flat, this));
            if (CritChance != 0)
                character.CritChance.AddModifier(new StatModifier(statBonuses[LevelUpModType.CritChance] * CritChance, StatModType.Flat, this));
            if (Health != 0)
                character.HealthPoints.AddModifier(new StatModifier(statBonuses[LevelUpModType.Health] * Health, StatModType.Flat, this));
            if (Resistance != 0)
                character.DamageReduction.AddModifier(new StatModifier(statBonuses[LevelUpModType.Resistance] * Resistance, StatModType.InverseProp, this));
            if (DodgeChance != 0)
                character.DodgeChance.AddModifier(new StatModifier(statBonuses[LevelUpModType.DodgeChance] * DodgeChance, StatModType.InverseProp, this));
            if (Stamina != 0)
                character.Stamina.AddModifier(new StatModifier(statBonuses[LevelUpModType.Stamina] * Stamina, StatModType.Flat, this));
            if (Mana != 0)
                character.Mana.AddModifier(new StatModifier(statBonuses[LevelUpModType.Mana] * Mana, StatModType.Flat, this));
        }
    }
}
