using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AE.Items
{
    public class Vendor : MonoBehaviour
    {
        public float SellValueMultiplier;
        public List<Item> Inventory;
        public int Money;

        public event Action InventoryUpdateEvent;

        private int inventorySize = 3;
        public bool AddItem(Item item)
        {
            Inventory.Add(item);
            InventoryUpdateEvent?.Invoke();
            return true;
        }
        public bool RemoveItem(Item item)
        {
            if (Inventory.Remove(item))
            {
                InventoryUpdateEvent?.Invoke();
                return true;
            }
            return false;
        }

        private void Start()
        {
            for (int i = 0; i < inventorySize; i++)
            {
                Item item = new Item();
            }
        }
    }
}
