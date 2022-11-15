using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace AE.Items
{
    public class InventoryHolder
    {
        public List<Item> Inventory = new List<Item> { };

        public event Action InventoryUpdateEvent;
        public bool AddItem(Item item)
        {
            if (item is null)
                return false;

            Inventory.Insert(SearchInsertIndex(item), item);
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

        private int SearchInsertIndex(Item item)
        {
            if (Inventory.Count == 0)
                return 0;

            int i = 0;

            for (; i < Inventory.Count; i++)
            {
                if (Inventory[i].Tier < item.Tier)
                    break;
                else if (Inventory[i].Tier == item.Tier)
                {
                    if (Inventory[i].Type > item.Type)
                        break;
                    else if (Inventory[i].Type == item.Type)
                    {
                        if (Inventory[i].Class >= item.Class)
                            break;
                    }
                }
            }

            return i;
        }

        public void sortInventory()
        {
            Inventory = Inventory.OrderByDescending(item => item.Tier)
                .ThenBy(item => item.Type)
                .ThenBy(item => item.Class).ToList();
        }

        protected void triggerInventoryUpdateEvent()
        {
            InventoryUpdateEvent?.Invoke();
        }
    }
}
