using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AE.Items;
using Newtonsoft.Json;
using static AbilityStorage;

namespace AE.GameSave
{

    public enum SaveSlot
    {
        None = -1, AutoSave = 0, Slot1 = 1, Slot2 = 2, Slot3 = 3
    }

    public class SaveController
    {
        static SaveController() {
            string path = Path.Combine(Application.persistentDataPath, "no_topping_left_beef.dat");
            if (File.Exists(path)) {
                StreamReader reader = new StreamReader(path);
                data = JsonConvert.DeserializeObject<Dictionary<int, JSONSave>>(Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd())));
                reader.Close();
            } else {
                data = new Dictionary<int, JSONSave>();
            }
        }

        private static Dictionary<int, JSONSave> data;

        public static void StartNew() {
            SaveData.SetDefaults();
        }

        public static void DeleteSave(SaveSlot slot) {
            if (!IsSlotOccupied(slot))
                return;
            
            data.Remove((int) slot);
            SaveToFile();
        }

        public static void ActivateSave(SaveSlot slot) {
            if (!IsSlotOccupied(slot)) {
                StartNew();
                return;
            }

            SaveData.Load(slot, data[(int) slot]);
        }

        public static void SaveCurrentDataTo(SaveSlot slot) {
            if (slot == SaveSlot.None)
                throw new ArgumentException("Cannot save to slot None!");

            List<JSONItem> inventory = new List<JSONItem>(), equippedItems = new List<JSONItem>();
            foreach (Item item in SaveData.Inventory)
                inventory.Add(ToJSONItem(item));
            foreach (Item item in SaveData.EquippedItems.Values)
                equippedItems.Add(ToJSONItem(item));

            data[(int)slot] = new JSONSave {
                LastModified = DateTime.Now.Ticks,
                Money = SaveData.Money,
                Inventory = inventory,
                EquippedItems = equippedItems,
                OwnedAbilities = new List<AbilityName>(SaveData.OwnedAbilities),
                EquippedAbilities = new List<AbilityName>(SaveData.EquippedAbilities),
                LevelUpAbilitiesCount = SaveData.LevelUpAbilitiesCount,
                GameStage = SaveData.GameStage,
                LevelUpSystem = new JSONLevelUpSystem{
                    Level = SaveData.LevelUpSystem.Level,
                    Exp = SaveData.LevelUpSystem.Exp,
                    Bonuses = new JSONLevelUpSystemBonuses{
                        Damage = SaveData.LevelUpSystem.Damage,
                        CritChance = SaveData.LevelUpSystem.CritChance,
                        Health = SaveData.LevelUpSystem.Health,
                        Resistance = SaveData.LevelUpSystem.Resistance,
                        DodgeChance = SaveData.LevelUpSystem.DodgeChance,
                        Stamina = SaveData.LevelUpSystem.Stamina,
                        Mana = SaveData.LevelUpSystem.Mana,
                    }
                },
                DebtRemaining = SaveData.DebtRemaining,
            };

            SaveToFile();
        }

        public static void SaveToFile() {
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "no_topping_left_beef.dat"), Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings{ NullValueHandling = NullValueHandling.Include }))));
        }

        private static JSONItem ToJSONItem(Item item) {
            return new JSONItem{
                Class = item.Class,
                Tier = item.Tier,
                Type = item.Type,
                Bonuses = new JSONItemBonuses{
                    Damage = item.DamageBonus,
                    CritPercent = item.CritPercentBonus,
                    Armor = item.ArmorBonus,
                    Dodge = item.DodgeBonus,
                    Mana = item.ManaBonus,
                },
                Weight = item.Weight,
                Name = item.Name
            };
        }

        public static DateTime? GetLastModified(SaveSlot slot) {
            return IsSlotOccupied(slot) ? new DateTime(data[(int) slot].LastModified) : null;
        }

        public static bool IsSlotOccupied(SaveSlot slot) {
            return data.ContainsKey((int) slot);
        }

    }
}
