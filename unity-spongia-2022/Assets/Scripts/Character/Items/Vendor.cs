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
        public event Action MoneyUpdateEvent;
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
        protected void triggerMoneyUpdateEvent()
        {
            MoneyUpdateEvent?.Invoke();
        }
    }
}
