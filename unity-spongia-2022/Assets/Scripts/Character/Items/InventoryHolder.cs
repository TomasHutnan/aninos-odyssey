using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AE.Items
{
    public class InventoryHolder : MonoBehaviour
    {
        public List<Item> Inventory;

        public event Action InventoryUpdateEvent;
        public bool AddItem(Item item)
        {
            if (item is null)
                return false;

            Inventory.Add(item);
            triggerInventoryUpdateEvent();
            return true;
        }
        public bool RemoveItem(Item item)
        {
            if (item is null)
                return false;

            if (Inventory.Remove(item))
            {
                triggerInventoryUpdateEvent();
                return true;
            }
            return false;
        }

        protected void triggerInventoryUpdateEvent()
        {
            InventoryUpdateEvent?.Invoke();
        }
    }
}
