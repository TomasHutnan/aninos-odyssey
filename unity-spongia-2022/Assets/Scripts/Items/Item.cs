using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Inventory
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
        Gloves,
        Leggings,
        Boots,
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
        Doge,
        Mana,
        Weight,
    }

    public class Item : MonoBehaviour
    {
        public int DamageBonus;
        public int CritPercentBonus;

        public int ArmorBonus;
        public int DodgeBonus;

        public int ManaBonus;

        public int Weight;

        public ItemClass Class;
        public ItemTier Tier;
        public ItemType Type;
        public ItemUsage Usage;

        public bool equipped;
        public Character character;

        public Dictionary<ItemType, ItemUsage> ItemTypeToUsage = new Dictionary<ItemType, ItemUsage> { 
            {ItemType.Helmet, ItemUsage.Armor},
            {ItemType.Chestplate, ItemUsage.Armor},
            {ItemType.Gloves, ItemUsage.Armor},
            {ItemType.Leggings, ItemUsage.Armor},
            {ItemType.Boots, ItemUsage.Armor},
            {ItemType.Weapon, ItemUsage.Weapon},
        };

        public Dictionary<ItemUsage, StatType[]> RelevantStats = new Dictionary<ItemUsage, StatType[]>
        {
            {ItemUsage.Armor, new StatType[] {StatType.Armor, StatType.Doge, StatType.Mana, StatType.Weight } },
            {ItemUsage.Weapon, new StatType[] {StatType.Damage, StatType.Crit, StatType.Mana, StatType.Weight } },
        };

        public Item(ItemClass _class, ItemTier tier, ItemType type,
            int? damageBonus = null, int? critPercentBonus = null,
            int? armorBonus = null, int? dodgeBonus = null,
            int? manaBonus = null,
            float? weight = null)
        {
            Class = _class;
            Tier = tier;
            Type = type;
            Usage = Type == ItemType.Weapon ? ItemUsage.Weapon : ItemUsage.Armor;
            
            DamageBonus = damageBonus is not null ? (int)damageBonus :
                (GenerateStat(StatType.Damage) );
            CritPercentBonus = critPercentBonus is not null ? (int)critPercentBonus : GenerateStat(StatType.Damage);
            ArmorBonus = armorBonus is not null ? (int)armorBonus : GenerateStat(StatType.Damage);
            DodgeBonus = dodgeBonus is not null ? (int)dodgeBonus : GenerateStat(StatType.Damage);
            ManaBonus = manaBonus is not null ? (int)manaBonus : GenerateStat(StatType.Damage);
            Weight = weight is not null ? (int)weight : GenerateStat(StatType.Damage);
        }

        public int GenerateStat(StatType statusType)
        {
            return 0;
        }
    }
}

