using AE.Items;

using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Items 
{

    public class EquipmentStatRanges {
        static EquipmentStatRanges () {
            foreach (SourceFileDescriptor descriptor in _descriptors)
                descriptor.Load();
        }

        private static string _directory = "StatRanges";

        private static List<SourceFileDescriptor> _descriptors = new List<SourceFileDescriptor>{
            new SourceFileDescriptor("armor_armor", StatType.Armor, new ItemUsage[]{ItemUsage.Armor}),
            new SourceFileDescriptor("armor_dodge", StatType.Dodge, new ItemUsage[]{ItemUsage.Armor}),
            new SourceFileDescriptor("weapon_crit", StatType.Crit, new ItemUsage[]{ItemUsage.Weapon}),
            new SourceFileDescriptor("weapon_damage", StatType.Damage, new ItemUsage[]{ItemUsage.Weapon}),
            new SourceFileDescriptor("mana", StatType.Mana, new ItemUsage[]{ItemUsage.Armor, ItemUsage.Weapon}),
            new SourceFileDescriptor("weight", StatType.Weight, new ItemUsage[]{ItemUsage.Armor, ItemUsage.Weapon})
        };

        public static Dictionary<ItemUsage, Dictionary<StatType, Dictionary<ItemClass, Dictionary<ItemTier, Range>>>> Ranges = new Dictionary<ItemUsage, Dictionary<StatType, Dictionary<ItemClass, Dictionary<ItemTier, Range>>>>{
            {ItemUsage.Armor, new Dictionary<StatType, Dictionary<ItemClass, Dictionary<ItemTier, Range>>>()},
            {ItemUsage.Weapon, new Dictionary<StatType, Dictionary<ItemClass, Dictionary<ItemTier, Range>>>()}
        };

        public static Range GetRange(ItemUsage itemUsage, StatType statType, ItemClass itemClass, ItemTier itemTier) {
            return Ranges[itemUsage][statType][itemClass][itemTier];
        }

        public static float GenerateRandom(ItemUsage itemUsage, StatType statType, ItemClass itemClass, ItemTier itemTier) {
            return GetRange(itemUsage, statType, itemClass, itemTier).Pick();
        }
    
        public class Range {
            public float Min, Max;

            public Range(float min, float max) {
                this.Min = min;
                this.Max = max;
            }

            public float Pick() {
                return UnityEngine.Random.Range(Min, Max);
            }
        };

        private class SourceFileDescriptor {
            private string _name;
            private StatType _statType;
            private ItemUsage[] _itemUsages;

            public SourceFileDescriptor(string name, StatType statType, ItemUsage[] itemUsages) {
                _name = name;
                _statType = statType;
                _itemUsages = itemUsages;
            }

            public void Load() {
                string[] data = Resources.Load<TextAsset>(_directory + "/" + _name).text.Split("\n");

                string[] header = data[0].Split(",");
                ItemClass[] classes = new ItemClass[header.Length - 1];
                for (int i = 1; i < header.Length; i++)
                    classes[i-1] = (ItemClass) Enum.Parse(typeof(ItemClass), Capitalize(header[i]));
                
                Dictionary<ItemClass, Dictionary<ItemTier, Range>> local = new Dictionary<ItemClass, Dictionary<ItemTier, Range>>();
                foreach (object clazz in Enum.GetValues(typeof(ItemClass)))
                    local.Add((ItemClass) clazz, new Dictionary<ItemTier, Range>());

                for (int i = 1; i < data.Length; i++) {
                    string[] line = data[i].Split(","), next = data[i+1].Split(",");
                    if (line[0].ToLower() == "max")
                        break;

                    ItemTier tier = (ItemTier) Enum.Parse(typeof(ItemTier), Capitalize(line[0]));
                    
                    for (int j = 1; j < line.Length; j++)
                        local[classes[j-1]].Add(tier, new Range(float.Parse(line[j], CultureInfo.InvariantCulture), float.Parse(next[j], CultureInfo.InvariantCulture)));
                }

                foreach (ItemUsage usage in _itemUsages)
                    Ranges[usage].Add(_statType, local);
            }
        };

        private static string Capitalize(string str) {
            return str.Length == 1 ? str[0].ToString().ToUpper() : str[0].ToString().ToUpper() + str.Substring(1);
        }
    }

}