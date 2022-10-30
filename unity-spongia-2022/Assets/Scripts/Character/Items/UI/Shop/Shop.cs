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

        private ItemShowcase sellableItemShowcase = null;

        private void Start()
        {
            for (int i = 0; i < itemShowcases.Length; i++)
            {
                Item _item = new Item
                (
                    _class: itemShowcases[i].randomizeClass ? null : itemShowcases[i].Class,
                    tier: GameManager.GameStage,
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
            EventManager.EventManager.TriggerItemPromptQuestion(_item, PromptType.Buy);
        }

        private void handlePromptAnswer(Item _item, PromptType _promptType, bool _answer)
        {
            if (_answer || _promptType == PromptType.Sell || _item == sellableItemShowcase.Item)
            {
                c.BuyItem(_item, (int)Mathf.Round(_item.value * GameManager.ShopValueMultiplier));
                inventoryHolder.RemoveItem(_item);
                sellableItemShowcase.Item = null;
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

            inventoryHolder = GetComponent<InventoryHolder>();
        }
    }
}
