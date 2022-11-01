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
                    border.enabled = false;
                    costText.enabled = false;
                }

                else
                {
                    costText.text = "<color=#FFD700>" + ((int)Mathf.Round(_item.value * GameManager.ShopValueMultiplier)).ToString() + " $</color>";
                    costText.enabled = true;
                    image.sprite = ItemImages.GetIcon(_item.Type, _item.Class);
                    image.enabled = true;
                    border.sprite = ItemImages.GetIconBorder(_item.Tier);
                    border.enabled = true;
                }
            }
        }

        protected void OnValidate()
        {
            costText = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
