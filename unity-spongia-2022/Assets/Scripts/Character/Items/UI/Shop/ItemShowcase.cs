using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AE.Items.UI.Shop
{
    public class ItemShowcase : ItemSlot
    {
        [SerializeField] TextMeshProUGUI costText;

        public ItemType Type;
        public bool randomizeClass;
        public ItemClass Class;

        public override Item Item
        {
            get { return _item; }
            set
            {
                _item = value;

                if (_item == null)
                {
                    image.enabled = false;
                    costText.enabled = false;
                }
                else
                {
                    costText.text = "<color=#FFD700>" + ((int)Mathf.Round(_item.value * GameManager.ShopValueMultiplier)).ToString() + " $</color>";
                    image.sprite = _item.Image;
                    image.enabled = true;
                    costText.enabled = true;
                }
            }
        }

        protected override void OnValidate()
        {
            costText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
