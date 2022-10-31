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
                data = JsonConvert.DeserializeObject<JSONBase>(reader.ReadToEnd());
                reader.Close();
            } else {
                data = new JSONBase {
                    AutoSave = null,
                    Saves = new Dictionary<int, JSONSave>()
                };
            }
        }

        private static JSONBase data;

        public static void StartNew() {
            SaveData.SetDefaults();
        }

        public static void ActivateSave(SaveSlot slot) {
            if (!isSlotOccupied(slot)) {
                StartNew();
                return;
            }

            SaveData.Load(data.Saves[(int) slot]);
        }

        public static bool isSlotOccupied(SaveSlot slot) {
            return slot == SaveSlot.AutoSave ? data.AutoSave != null : data.Saves.ContainsKey((int) slot);
        }

    }
}
