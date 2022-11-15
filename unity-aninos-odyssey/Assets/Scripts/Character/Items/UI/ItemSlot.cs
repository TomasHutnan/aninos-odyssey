using AE.EventManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AE.Items.UI
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Image image;
        [SerializeField] protected Image border;

        private Sprite defaultSprite;

        public event Action<Item> OnItemRightClickedEvent;
        public event Action<Item> OnItemLeftClickedEvent;

        protected Item _item;
        public virtual Item Item
        {
            get { return _item; }
            set
            {
                _item = value;

                if (_item == null)
                {
                    image.enabled = false;
                    border.enabled = false;
                }

                else
                {
                    image.sprite = ItemImages.GetIcon(_item.Type, _item.Class);
                    image.enabled = true;
                    border.sprite = ItemImages.GetIconBorder(_item.Tier);
                    border.enabled = true;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData != null && Item != null)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    OnItemLeftClickedEvent?.Invoke(Item);
                    EventManager.EventManager.TriggerItemSlotExit();
                }
                else if (eventData.button == PointerEventData.InputButton.Right)
                {
                    OnItemRightClickedEvent?.Invoke(Item);
                    EventManager.EventManager.TriggerItemSlotExit();
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Item != null)
                EventManager.EventManager.TriggerItemSlotEnter(Item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            EventManager.EventManager.TriggerItemSlotExit();
        }
    }
}
