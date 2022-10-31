using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using AE.GameSave;
using AE.EventManager;

namespace AE.Items.UI
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] Transform ItemSlotsGrid;
        [SerializeField] TextMeshProUGUI CurrentPageText;
        [SerializeField] ItemSlot[] itemSlots;
        [Space]
        [SerializeField] Character c;

        public event Action<Item> OnItemRightClickedEvent;
        public event Action<Item> OnItemLeftClickedEvent;

        private int inventoryPagesCount;
        private int currentPage = 0;

        private Item sellableItem = null;

        private void Start()
        {
            RefreshUI();
        }
        private void OnEnable()
        {
            if (c is null)
                c = GameManager.PlayerCharacter;

            c.InventoryUpdateEvent += RefreshUI;

            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].OnItemRightClickedEvent += OnItemRightClickedEvent;
                itemSlots[i].OnItemLeftClickedEvent += OnItemLeftClickedEvent;

                itemSlots[i].OnItemRightClickedEvent += handleRightClick;
                itemSlots[i].OnItemLeftClickedEvent += handleLeftClick;
            }

            EventManager.EventManager.ItemPromptAnswerEvent += handlePromptAnswer;

            RefreshUI();
        }

        private void OnDisable()
        {
            c.InventoryUpdateEvent -= RefreshUI;

            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].OnItemRightClickedEvent -= OnItemRightClickedEvent;
                itemSlots[i].OnItemLeftClickedEvent -= OnItemLeftClickedEvent;

                itemSlots[i].OnItemRightClickedEvent -= handleRightClick;
                itemSlots[i].OnItemLeftClickedEvent -= handleLeftClick;
            }

            EventManager.EventManager.ItemPromptAnswerEvent -= handlePromptAnswer;
        }

        private void RefreshUI()
        {
            updatePagesCount();

            if (currentPage > inventoryPagesCount)
                currentPage = inventoryPagesCount;

            CurrentPageText.text = $"{currentPage + 1} / {inventoryPagesCount + 1}";

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

        private void handleLeftClick(Item _item)
        {
            c.EquipItem(_item);
        }
        private void handleRightClick(Item _item)
        {
            if (SaveData.ConfirmSell)
            {
                sellableItem = _item;
                EventManager.EventManager.TriggerItemPromptQuestion(_item, PromptType.Sell);
            }
            else
            {
                c.SellItem(_item);
            }
        }

        private void handlePromptAnswer(Item _item, PromptType _promptType, bool _answer)
        {
            if (_promptType != PromptType.Sell || !_answer || _item != sellableItem)
                return;

            sellableItem = null;
            c.SellItem(_item);
        }

        public void NextPage()
        {
            if (inventoryPagesCount == 0)
            {
                if (currentPage == 0)
                    return;
                currentPage = 0;
            }
            else if (currentPage < inventoryPagesCount)
                currentPage += 1;
            else
                currentPage = 0;

            RefreshUI();
        }
        public void PreviousPage()
        {
            if (inventoryPagesCount == 0)
            {
                if (currentPage == 0)
                    return;
                currentPage = 0;
            }
            else if (currentPage > 0)
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
