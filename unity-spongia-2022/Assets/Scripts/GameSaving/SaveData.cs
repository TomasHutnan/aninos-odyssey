using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Items {
    public class SaveData {

        public static int Money = 0;
        public static List<Item> Inventory = new List<Item>();
        public static Dictionary<ItemType, Item> EquippedItems = new Dictionary<ItemType, Item>();

        // + stage, xp/amount of fights done in a stage?

    }
}