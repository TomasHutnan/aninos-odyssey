using System;
using System.Collections;
using System.Collections.Generic;
using AE.CharacterStats;
using AE.Items;
using static AbilityStorage;

namespace AE.GameSave
{
    public class SaveData
    {
        private static SaveSlot slot = SaveSlot.None;

        public static int Money = 0;

        public static List<Item> Inventory = new List<Item>();
        public static Dictionary<ItemType, Item> EquippedItems = new Dictionary<ItemType, Item>();

        public static HashSet<AbilityName> OwnedAbilities = new HashSet<AbilityName>(), EquippedAbilities = new HashSet<AbilityName>();

        public static ItemTier GameStage = ItemTier.Mortal;
        public static LevelUpSystem LevelUpSystem = new LevelUpSystem();

        public static Character PlayerCharacter = new Character();

        public static void SetDefaults() {
            slot = SaveSlot.None;
            Money = 0;
            Inventory = new List<Item>();
            EquippedItems = new Dictionary<ItemType, Item>();
            OwnedAbilities = new HashSet<AbilityName>();
            EquippedAbilities = new HashSet<AbilityName>();
            GameStage = ItemTier.Mortal;
            LevelUpSystem = new LevelUpSystem();

            PlayerCharacter = new Character();
            PlayerCharacter.PostInit();
        }

        public static bool HasSlot() {
            return slot != SaveSlot.None;
        }

        public static void AutoSave() {
            Save(SaveSlot.AutoSave);
        }
        public static void Save() {
            if (slot == SaveSlot.None)
                throw new ArgumentException("The current save does not have any slot associated!");

            Save(slot);
        }

        public static void Save(SaveSlot slot) {
            Money = PlayerCharacter.Money;
            Inventory = PlayerCharacter.Inventory;
            EquippedItems = PlayerCharacter.EquippedItems;
            OwnedAbilities = PlayerCharacter.UnlockedAbilities;
            EquippedAbilities = PlayerCharacter.EquippedAbilities;
            LevelUpSystem = PlayerCharacter.LevelUpSystem;

            if (slot == SaveSlot.None)
                throw new ArgumentException("Cannot save to slot None!");

            SaveData.slot = slot;
            SaveController.SaveCurrentDataTo(slot);
        }

        public static void Load(SaveSlot slot, JSONSave save) {
            SaveData.slot = slot;
            Money = save.Money;
            
            Inventory = new List<Item>(save.Inventory.Count);
            foreach (JSONItem item in save.Inventory) {
                Inventory.Add(new Item(
                    item.Class, item.Tier, item.Type,
                    item.Bonuses.Damage, item.Bonuses.CritPercent, item.Bonuses.Armor, item.Bonuses.Dodge, item.Bonuses.Mana,
                    item.Weight, item.Name
                ));
            }

            EquippedItems = new Dictionary<ItemType, Item>();
            foreach (JSONItem item in save.EquippedItems) {
                EquippedItems.Add(item.Type, new Item(
                    item.Class, item.Tier, item.Type,
                    item.Bonuses.Damage, item.Bonuses.CritPercent, item.Bonuses.Armor, item.Bonuses.Dodge, item.Bonuses.Mana,
                    item.Weight, item.Name
                ));
            }

            OwnedAbilities = new HashSet<AbilityName>(save.OwnedAbilities);
            EquippedAbilities = new HashSet<AbilityName>(save.EquippedAbilities);
            GameStage = save.GameStage;
            LevelUpSystem = new LevelUpSystem(null, save.LevelUpSystem.Level, save.LevelUpSystem.Exp,
                save.LevelUpSystem.Bonuses.Damage, save.LevelUpSystem.Bonuses.CritChance, save.LevelUpSystem.Bonuses.Health, save.LevelUpSystem.Bonuses.Resistance, save.LevelUpSystem.Bonuses.DodgeChance, save.LevelUpSystem.Bonuses.Stamina, save.LevelUpSystem.Bonuses.Mana);

            PlayerCharacter = new Character(
                Money, EquippedItems, Inventory, LevelUpSystem,
                OwnedAbilities, EquippedAbilities);
            PlayerCharacter.PostInit();
        }
    }
}