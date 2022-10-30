using AE.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Items.UI
{
    public class EquipmentPanel : MonoBehaviour
    {
        [SerializeField] Transform[] equipmentSlotsGrids;

        [SerializeField] Dictionary<ItemType, EquipmentSlot> equipmentSlots = new Dictionary<ItemType, EquipmentSlot> { };

        public event Action<Item> OnItemRightClickedEvent;
        public event Action<Item> OnItemLeftClickedEvent;

        [SerializeField] Character c;

        private void Start()
        {
            RefreshUI();
        }

        private void OnEnable()
        {
            if (c is null)
                c = GameManager.PlayerCharacter;

            c.EquipmentUpdateEvent += RefreshUI;

            foreach (EquipmentSlot equipmentSlot in equipmentSlots.Values)
            {
                equipmentSlot.OnItemRightClickedEvent += OnItemRightClickedEvent;
                equipmentSlot.OnItemLeftClickedEvent += OnItemLeftClickedEvent;

                equipmentSlot.OnItemRightClickedEvent += handleUnequip;
                equipmentSlot.OnItemLeftClickedEvent += handleUnequip;
            }
        }

        private void OnDisable()
        {
            c.EquipmentUpdateEvent -= RefreshUI;

            foreach (EquipmentSlot equipmentSlot in equipmentSlots.Values)
            {
                equipmentSlot.OnItemRightClickedEvent -= OnItemRightClickedEvent;
                equipmentSlot.OnItemLeftClickedEvent -= OnItemLeftClickedEvent;

                equipmentSlot.OnItemRightClickedEvent -= handleUnequip;
                equipmentSlot.OnItemLeftClickedEvent -= handleUnequip;
            }
        }

        private void RefreshUI()
        {
            foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
            {
                if (c.EquippedItems.ContainsKey(itemType) && c.EquippedItems[itemType] is not null)
                {
                    equipmentSlots[itemType].Item = c.EquippedItems[itemType];
                }
                else
                    equipmentSlots[itemType].Item = null;
            }
        }

        private void handleUnequip(Item item)
        {
            c.UnequipItem(item);
        }

        private void OnValidate()
        {
            foreach (Transform slotGrid in equipmentSlotsGrids)
                foreach (EquipmentSlot equipmentSlot in slotGrid.GetComponentsInChildren<EquipmentSlot>())
                {
                    equipmentSlots[equipmentSlot.ItemType] = equipmentSlot;
                }
        }
    }
}
