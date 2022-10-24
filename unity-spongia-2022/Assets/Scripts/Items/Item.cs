using AE.CharacterStats;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Items
{
    public enum ItemClass
    {
        Fighter,
        Tank,
        Rogue,
        Priest,
    }

    public enum ItemTier
    {
        Mortal,
        Earth,
        Heaven,
        God,
    }

    public enum ItemType
    {
        Helmet,
        Chestplate,
        Leggings,
        Boots,
        Shield,
        Weapon,
    }

    public enum ItemUsage
    {
        Armor,
        Weapon,
    }

    public enum StatType
    {
        Damage,
        Crit,
        Armor,
        Dodge,
        Mana,
        Weight,
    }

    public class Item : MonoBehaviour
    {
        public float DamageBonus;
        public float CritPercentBonus;

        public float ArmorBonus;
        public float DodgeBonus;

        public float ManaBonus;

        public float Weight;

        public ItemClass Class;
        public ItemTier Tier;
        public ItemType Type;
        public ItemUsage Usage;

        public int value;

        public bool equipped;
        
        public Sprite icon;
        public Sprite image;

        public Dictionary<ItemType, ItemUsage> ItemTypeToUsage = new Dictionary<ItemType, ItemUsage> { 
            {ItemType.Helmet, ItemUsage.Armor},
            {ItemType.Chestplate, ItemUsage.Armor},
            {ItemType.Shield, ItemUsage.Armor},
            {ItemType.Leggings, ItemUsage.Armor},
            {ItemType.Boots, ItemUsage.Armor},
            {ItemType.Weapon, ItemUsage.Weapon},
        };

        public Item(ItemClass _class, ItemTier tier, ItemType type,
            float? damageBonus = null, float? critPercentBonus = null,
            float? armorBonus = null, float? dodgeBonus = null,
            float? manaBonus = null,
            float? weight = null)
        {
            Class = _class;
            Tier = tier;
            Type = type;
            Usage = Type == ItemType.Weapon ? ItemUsage.Weapon : ItemUsage.Armor;
            
            DamageBonus = damageBonus is not null ? (float)damageBonus :
                (Usage == ItemUsage.Weapon ? GenerateStat(StatType.Damage) : 0);
            CritPercentBonus = critPercentBonus is not null ? (float)critPercentBonus :
                (Usage == ItemUsage.Weapon ? GenerateStat(StatType.Crit) : 0);
            ArmorBonus = armorBonus is not null ? (float)armorBonus :
                (Usage == ItemUsage.Armor ? GenerateStat(StatType.Armor) : 0);
            DodgeBonus = dodgeBonus is not null ? (float)dodgeBonus :
                (Usage == ItemUsage.Armor ? GenerateStat(StatType.Dodge) : 0);
            ManaBonus = manaBonus is not null ? (float)manaBonus : GenerateStat(StatType.Mana);
            Weight = weight is not null ? (float)weight : GenerateStat(StatType.Damage);
        }

        public float GenerateStat(StatType statType)
        {
            return (float)Math.Round(EquipmentStatRanges.GenerateRandom(Usage, statType, Class, Tier), 1);
        }

        public void Equip(Character c)
        {
            if (DamageBonus != 0)
                c.Damage.AddModifier(new StatModifier(DamageBonus, StatModType.Flat, this));
            if (CritPercentBonus != 0)
                c.CritChance.AddModifier(new StatModifier(CritPercentBonus, StatModType.Flat, this));
            if (ArmorBonus != 0)
                c.DamageReduction.AddModifier(new StatModifier(ArmorBonus, StatModType.InverseProp, this));
            if (DodgeBonus != 0)
                c.DodgeChance.AddModifier(new StatModifier(DodgeBonus, StatModType.InverseProp, this));
            if (ManaBonus != 0)
                c.Mana.AddModifier(new StatModifier(ManaBonus, StatModType.Flat, this));
            if (Weight != 0)
                c.Weight.AddModifier(new StatModifier(Weight, StatModType.Flat, this));
        }

        public void Unequip(Character c)
        {
            c.Damage.RemoveAllModifiersFromSource(this);
            c.CritChance.RemoveAllModifiersFromSource(this);
            c.DamageReduction.RemoveAllModifiersFromSource(this);
            c.DodgeChance.RemoveAllModifiersFromSource(this);
            c.Mana.RemoveAllModifiersFromSource(this);
            c.Weight.RemoveAllModifiersFromSource(this);
        }
    }
}

