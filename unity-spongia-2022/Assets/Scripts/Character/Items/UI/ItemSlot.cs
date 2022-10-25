using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AE.Items;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image image;

    public event Action<Item> OnClickEvent;

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
        if (eventData != null && 
           (eventData.button == PointerEventData.InputButton.Left || 
            eventData.button == PointerEventData.InputButton.Right))
        {
            if (Item != null && OnClickEvent != null)
                OnClickEvent(Item);
        }
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
    }
}
