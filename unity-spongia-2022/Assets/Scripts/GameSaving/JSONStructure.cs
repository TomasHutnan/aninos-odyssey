using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AE.Items;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static AbilityStorage;

namespace AE.GameSave
{
    public class JSONSave {
        public long LastModified;
        public int Money {get; set;}
        public List<JSONItem> Inventory {get; set;}
        public List<JSONItem> EquippedItems {get; set;}
        public List<AbilityName> OwnedAbilities {get; set;}
        public List<AbilityName> EquippedAbilities {get; set;}
        public int LevelUpAbilitiesCount;
        [JsonConverter(typeof(StringEnumConverter))]
        public ItemTier GameStage {get; set;}
        public JSONLevelUpSystem LevelUpSystem {get; set;}
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
        public float Damage {get; set;}
        public float CritPercent {get; set;}
        public float Armor {get; set;}
        public float Dodge {get; set;}
        public float Mana {get; set;}
    }
}
