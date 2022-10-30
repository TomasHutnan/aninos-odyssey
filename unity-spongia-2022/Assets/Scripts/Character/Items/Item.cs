using AE.CharacterStats;
using AE.Utils;

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
        Armguards,
        Shinguards,
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

    public class Item
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
        
        public Sprite Icon;
        public Sprite Image;

        public string Name;

        public Dictionary<ItemType, ItemUsage> ItemTypeToUsage = new Dictionary<ItemType, ItemUsage> { 
            {ItemType.Helmet, ItemUsage.Armor},
            {ItemType.Chestplate, ItemUsage.Armor},
            {ItemType.Armguards, ItemUsage.Armor},
            {ItemType.Shinguards, ItemUsage.Armor},
            {ItemType.Shield, ItemUsage.Armor},
            {ItemType.Weapon, ItemUsage.Weapon},
        };

        public Item(ItemClass? _class = null, ItemTier? tier = null, ItemType? type = null,
            float? damageBonus = null, float? critPercentBonus = null,
            float? armorBonus = null, float? dodgeBonus = null,
            float? manaBonus = null,
            float? weight = null,
            string name = null)
        {
            Class = _class is not null ? (ItemClass)_class : EnumUtils.RandomEnumValue<ItemClass>();
            Tier = tier is not null ? (ItemTier)tier : EnumUtils.RandomEnumValue<ItemTier>(); ;
            Type = type is not null ? (ItemType)type : EnumUtils.RandomEnumValue<ItemType>(); ;
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
            Weight = weight is not null ? (float)weight : GenerateStat(StatType.Weight);

            Name = name is not null ? (string)name : GenerateName();

            Icon = Resources.Load<Sprite>("Items\\icon");

            float sumBonus = DamageBonus + CritPercentBonus + ArmorBonus + DodgeBonus + ManaBonus - Weight;
            value = (int)Math.Round(Math.Pow(Math.Abs(sumBonus), 1+0.1*(int)Tier));
        }

        private float GenerateStat(StatType statType)
        {
            return (float)Math.Round(EquipmentStatRanges.GenerateRandom(Usage, statType, Class, Tier), 1);
        }

        private string GenerateName()
        {
            return EquipmentNames.GetRandom();
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

