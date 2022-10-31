using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AE.Items;

namespace AE.GameSave
{
    public class JSONStructure
    {

        public class JSONBase {
            public JSONSave AutoSave {get; set;}
            public Dictionary<int, JSONSave> Saves {get; set;}
        }
        public class JSONSave {
            public int LastModified;
            public int Money {get; set;}
            public List<JSONItem> Inventory {get; set;}
            public static List<JSONItem> EquippedItems {get; set;}
            public List<int> OwnedAbilities {get; set;}
            public List<int> EquippedAbilities {get; set;}
            [JsonConverter(typeof(StringEnumConverter))]
            public ItemTier GameStage {get; set;}
            public bool ConfirmSell {get; set;};
            public JSONLevelUpSystem LevelUpSystem {get; set;};
            public List<bool> IsMuted {get; set;};
        }

        public class JSONLevelUpSystem {
            public int Level {get; set;}
            public int Exp {get; set;}
            public JSONLevelUpSystemBonuses Bonuses {get; set;}
        }
        public class JSONLevelUpSystemBonuses {
            public int Damage {get; set;}
            public int CritChance {get; set;}
            public int Health {get; set;}
            public int Resistance {get; set;}
            public int DodgeChance {get; set;}
            public int Stamina {get; set;}
            public int Mana {get; set;}
        }

        public class JSONItem {
            [JsonConverter(typeof(StringEnumConverter))]
            public ItemClass Class {get; set;}
            [JsonConverter(typeof(StringEnumConverter))]
            public ItemTier Tier {get; set;}
            [JsonConverter(typeof(StringEnumConverter))]
            public ItemType Type {get; set;}
            public JSONItemBonuses Bonuses {get; set;}
            public float Weight {get; set;}
            public string Name {get; set;}
        }
        public class JSONItemBonuses {
            float Damage {get; set;}
            float CritPercent {get; set;}
            float Armor {get; set;}
            float Dodge {get; set;}
            float Mana {get; set;}
        }
        
    }
}
