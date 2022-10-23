using AE.Items;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Item
{
    public static class EquipmentStatRanges
    {
        static EquipmentStatRanges ()
        {
            // Ranges[ItemUsage.Armor][StatType.Armor][]
        }

        static string directory = "StatRanges\\";

        static Dictionary<ItemUsage, Dictionary<StatType, string>> filePaths =
            new Dictionary<ItemUsage, Dictionary<StatType, string>>
            {
                {ItemUsage.Armor, new Dictionary<StatType, string> {
                    { StatType.Armor, directory+"armor_armor.csv" },
                    { StatType.Dodge, directory+"armor_dodge.csv" },
                    { StatType.Mana, directory+"mana.csv" },
                    { StatType.Weight, directory+"weight.csv" },
                }},
                {ItemUsage.Weapon, new Dictionary<StatType, string> {
                    { StatType.Damage, directory+"weapon_damage.csv" },
                    { StatType.Crit, directory+"weapon_crit.csv" },
                    { StatType.Mana, directory+"mana.csv" },
                    { StatType.Weight, directory+"weight.csv" },
                }},
            };

        static Dictionary<string, ItemClass> stringToItemClass = new Dictionary<string, ItemClass>
        {
            {"fighter", ItemClass.Fighter },
            {"tank", ItemClass.Tank },
            {"rogue", ItemClass.Rogue },
            {"priest", ItemClass.Priest },
        };

        static Dictionary<string, ItemTier> stringToItemTier = new Dictionary<string, ItemTier>
        {
            {"mortal", ItemTier.Mortal },
            {"earth", ItemTier.Earth },
            {"heaven", ItemTier.Heaven },
            {"god", ItemTier.God },
        };

        public static Dictionary<ItemUsage, Dictionary<StatType, Dictionary<ItemClass, Dictionary<ItemTier, string>>>> Ranges;
    
        private class Range
        {
            float min, max;

            public float RandomFloat()
            {
                return Random.Range(min, max);
            }
        };
    }

}
