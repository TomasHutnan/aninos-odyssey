using System;
using System.Collections;
using System.Collections.Generic;
using AE.CharacterStats;
using AE.Items;

namespace AE.GameSave
{
    public class SaveData
    {
        private static SaveSlot slot = SaveSlot.None;

        public static int Money = 0;
        public static List<Item> Inventory = new List<Item>();
        public static Dictionary<ItemType, Item> EquippedItems = new Dictionary<ItemType, Item>();

        public static HashSet<int> OwnedAbilities = new HashSet<int>(), EquippedAbilities = new HashSet<int>();

        public static ItemTier GameStage = ItemTier.Mortal;

        public static bool ConfirmSell = true;

        public static LevelUpSystem LevelUpSystem = new LevelUpSystem();

        public static bool[] IsMuted = { false, false };
        // + stage, xp/amount of fights done in a stage?

        public static void SetDefaults() {
            slot = SaveSlot.None;
            Money = 0;
            Inventory = new List<Item>();
            EquippedItems = new Dictionary<ItemType, Item>();
            OwnedAbilities = new HashSet<int>();
            EquippedAbilities = new HashSet<int>();
            GameStage = ItemTier.Mortal;
            ConfirmSell = true;
            LevelUpSystem = new LevelUpSystem();
            IsMuted = new bool[]{false, false};
        }

        public static bool hasSlot() {
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
            SaveData.slot = slot;
            // call controller
        }

        public static void Load(JSONSave save) {
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

            OwnedAbilities = new HashSet<int>(save.OwnedAbilities);
            EquippedAbilities = new HashSet<int>(save.EquippedAbilities);
            GameStage = save.GameStage;
            ConfirmSell = save.ConfirmSell;
            LevelUpSystem = new LevelUpSystem(null, save.LevelUpSystem.Level, save.LevelUpSystem.Exp,
                save.LevelUpSystem.Bonuses.Damage, save.LevelUpSystem.Bonuses.CritChance, save.LevelUpSystem.Bonuses.Health, save.LevelUpSystem.Bonuses.Resistance, save.LevelUpSystem.Bonuses.DodgeChance, save.LevelUpSystem.Bonuses.Stamina, save.LevelUpSystem.Bonuses.Mana);
            IsMuted = save.IsMuted.ToArray();
        }
    }
}