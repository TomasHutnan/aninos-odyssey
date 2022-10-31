using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AE.Items;
using Newtonsoft.Json;

namespace AE.GameSave
{

    public enum SaveSlot
    {
        None = -1, AutoSave = 0, Slot1 = 1, Slot2 = 2, Slot3 = 3
    }

    public class SaveController
    {
        static SaveController() {
            string path = Path.Combine(Application.persistentDataPath, "no_topping_left_beef.json");
            if (File.Exists(path)) {
                StreamReader reader = new StreamReader(path);
                data = JsonConvert.DeserializeObject<Dictionary<int, JSONSave>>(reader.ReadToEnd());
                reader.Close();
            } else {
                data = new Dictionary<int, JSONSave>();
            }
        }

        private static Dictionary<int, JSONSave> data;

        public static void StartNew() {
            SaveData.SetDefaults();
        }

        public static void ActivateSave(SaveSlot slot) {
            if (!isSlotOccupied(slot)) {
                StartNew();
                return;
            }

            SaveData.Load(data[(int) slot]);
        }

        public static void SaveDataTo(SaveSlot slot) {
            List<JSONItem> inventory = new List<JSONItem>(), equippedItems = new List<JSONItem>();

            data.Add((int) slot, new JSONSave{
                LastModified = DateTime.Now.Ticks,
                Money = SaveData.Money,
                Inventory = inventory,
                EquippedItems = equippedItems,
                OwnedAbilities = new List<int>(SaveData.OwnedAbilities),
                EquippedAbilities = new List<int>(SaveData.EquippedAbilities),
                GameStage = SaveData.GameStage,
                LevelUpSystem = new JSONLevelUpSystem{

                }
            });
        }

        public static bool isSlotOccupied(SaveSlot slot) {
            return data.ContainsKey((int) slot);
        }

    }
}
