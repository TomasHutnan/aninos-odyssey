using System.Collections;
using System.Collections.Generic;
using AE.CharacterStats;
using AE.Items;

namespace AE.GameSave
{
    public class SaveData
    {

        public static int Money = 0;
        public static List<Item> Inventory = new List<Item>();
        public static Dictionary<ItemType, Item> EquippedItems = new Dictionary<ItemType, Item>();

        public static ItemTier GameStage = ItemTier.Mortal;

        public static bool ConfirmSell = true;

        public static LevelUpSystem LevelUpSystem = new LevelUpSystem();
        // + stage, xp/amount of fights done in a stage?

    }
}