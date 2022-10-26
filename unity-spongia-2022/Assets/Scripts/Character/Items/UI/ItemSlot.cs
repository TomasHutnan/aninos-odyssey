using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

namespace AE.Items.UI
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image image;

        public event Action<Item> OnItemRightClickedEvent;
        public event Action<Item> OnItemLeftClickedEvent;

        private Item _item;
        public Item Item
        {
            get { return _item; }
            set
            {
                _item = value;

                if (_item == null)
                    image.enabled = false;
                else
                {
                    image.sprite = _item.Icon;
                    image.enabled = true;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData != null && Item != null)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                    OnItemLeftClickedEvent(Item);
                else if (eventData.button == PointerEventData.InputButton.Right)
                    OnItemRightClickedEvent(Item);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }

        protected virtual void OnValidate()
        {
            if (image == null)
                image = GetComponent<Image>();
        }
    }
}
