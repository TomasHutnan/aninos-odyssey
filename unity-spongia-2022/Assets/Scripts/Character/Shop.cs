using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Items
{
    public class Shop : InventoryHolder
    {
        private int inventorySize = 3;

        private void Start()
        {
            for (int i = 0; i < inventorySize; i++)
            {
                Item item = new Item(tier: GameManager.GameStage);
            }
            triggerInventoryUpdateEvent();
        }
    }
}
