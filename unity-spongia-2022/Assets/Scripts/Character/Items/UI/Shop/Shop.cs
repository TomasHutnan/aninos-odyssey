using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Items.UI.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] GameObject itemShowcaseParent;
        [SerializeField] ItemShowcase[] itemShowcases;

        [Space]
        [SerializeField] InventoryHolder inventoryHolder = new InventoryHolder();
        [SerializeField] Character c;

        private Item sellableItem = null;

        private void Start()
        {
            for (int i = 0; i < itemShowcases.Length; i++)
            {
                Item _item = new Item
                (
                    _class: itemShowcases[i].randomizeClass ? null : itemShowcases[i].Class,
                    tier: SaveData.GameStage,
                    type: itemShowcases[i].Type
                );
                inventoryHolder.AddItem(_item);
                itemShowcases[i].Item = _item;
            }
        }

        private void OnEnable()
        {
            if (c is null)
                c = GameManager.PlayerCharacter;

            for (int i = 0; i < itemShowcases.Length; i++)
            {
                itemShowcases[i].OnItemRightClickedEvent += handleItemClick;
                itemShowcases[i].OnItemLeftClickedEvent += handleItemClick;
            }

            EventManager.EventManager.ItemPromptAnswerEvent += handlePromptAnswer;
        }
        private void OnDisable()
        {
            for (int i = 0; i < itemShowcases.Length; i++)
            {
                itemShowcases[i].OnItemRightClickedEvent -= handleItemClick;
                itemShowcases[i].OnItemLeftClickedEvent -= handleItemClick;
            }

            EventManager.EventManager.ItemPromptAnswerEvent -= handlePromptAnswer;
        }

        private void handleItemClick(Item _item)
        {
            sellableItem = _item;
            EventManager.EventManager.TriggerItemPromptQuestion(_item, PromptType.Buy);
        }

        private void handlePromptAnswer(Item _item, PromptType _promptType, bool _answer)
        {
            if (!_answer || _promptType != PromptType.Buy || _item != sellableItem)
                return;

            if (!c.BuyItem(_item, (int)Mathf.Round(_item.value * GameManager.ShopValueMultiplier)))
                return;

            inventoryHolder.RemoveItem(_item);

            foreach (ItemShowcase itemShowcase in itemShowcases)
                if (itemShowcase.Item == sellableItem)
                {
                    itemShowcase.Item = null;
                    break;
                }
        }

        private void OnValidate()
        {
            if (itemShowcaseParent != null)
            {
                itemShowcases = itemShowcaseParent.GetComponentsInChildren<ItemShowcase>();
                for (int i = 0; i < itemShowcases.Length; i++)
                {
                    itemShowcases[i].name = itemShowcases[i].Type.ToString() + $" Showcase ({i + 1})";
                }
            }
        }
    }
}
