using AE.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Items.UI
{
    public class EquipmentSlot : ItemSlot
    {
        public ItemType ItemType;

        protected void OnValidate()
        {
            gameObject.name = ItemType.ToString() + " Slot";
        }
    }
}
