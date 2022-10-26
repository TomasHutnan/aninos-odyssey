using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AE.Items.UI
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] Transform ItemSlotsGrid;
        [SerializeField] TextMeshProUGUI CurrentPageText;
        [SerializeField] ItemSlot[] itemSlots;

        public event Action<Item> OnItemRightClickedEvent;
        public event Action<Item> OnItemLeftClickedEvent;

        [SerializeField] Character c;

        private int inventoryPagesCount;
        private int currentPage = 0;

        void Start()
        {
            if (c is null)
                c = GameManager.PlayerCharacter;

            c.InventoryUpdateEvent += RefreshUI;

            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].OnItemRightClickedEvent += OnItemRightClickedEvent;
                itemSlots[i].OnItemLeftClickedEvent += OnItemLeftClickedEvent;

                //itemSlots[i].OnItemRightClickedEvent += OnItemRightClickedEvent;
                itemSlots[i].OnItemLeftClickedEvent += handleLeftClick;
            }

            RefreshUI();
        }

        private void OnDisable()
        {
            c.InventoryUpdateEvent -= RefreshUI;
        }

        private void RefreshUI()
        {
            updatePagesCount();

            CurrentPageText.text = $"Page {currentPage + 1}";

            int i = 0;
            for (; i < c.Inventory.Count - itemSlots.Length * currentPage && i < itemSlots.Length; i++)
            {
                itemSlots[i].Item = c.Inventory[itemSlots.Length * currentPage + i];
            }
            for (; i < itemSlots.Length; i++)
            {
                itemSlots[i].Item = null;
            }
        }

        private void handleLeftClick(Item item)
        {
            c.EquipItem(item);
        }

        public void NextPage()
        {
            if (inventoryPagesCount == 0)
                return;

            if (currentPage < inventoryPagesCount)
                currentPage += 1;
            else
                currentPage = 0;

            RefreshUI();
        }
        public void PreviousPage()
        {
            if (inventoryPagesCount == 0)
                return;

            if (currentPage > 0)
                currentPage -= 1;
            else
                currentPage = inventoryPagesCount;

            RefreshUI();
        }

        private void OnValidate()
        {
            if (ItemSlotsGrid != null)
                itemSlots = ItemSlotsGrid.GetComponentsInChildren<ItemSlot>();
        }

        private void updatePagesCount()
        {
            int count = c.Inventory.Count;

            if (count <= itemSlots.Length)
                inventoryPagesCount = 0;
            else
                inventoryPagesCount = (int)Math.Floor((float)(count / itemSlots.Length)) - ((count % itemSlots.Length == 0) ? 1 : 0);
        }
    }
}
