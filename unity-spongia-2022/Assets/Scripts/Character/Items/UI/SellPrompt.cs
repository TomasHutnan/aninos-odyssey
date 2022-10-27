using AE.EventManager;
using AE.GameSave;
using AE.Items;
using AE.Items.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPrompt : MonoBehaviour
{
    [SerializeField] Character c;
    [SerializeField] ItemSlot ItemSlot;

    private Item _item;
    public Item SellableItem
    {
        get { return _item; }
        set
        {
            _item = value;

            if (_item == null)
                gameObject.SetActive(false);
            else
            {
                ItemSlot.Item = _item;
            }
        }
    }

    bool? dontAskAgain = null;

    private void Start()
    {
        if (c is null)
            c = GameManager.PlayerCharacter;
    }

    public void ToggleDontAsk(bool value)
    {
        dontAskAgain = value;
    }
    public void Yes()
    {
        if (dontAskAgain == true)
            SaveData.ConfirmSell = false;

        c.SellItem(SellableItem);
        EventManager.TriggerItemSlotExit();
        gameObject.SetActive(false);
    }
    public void No()
    {
        gameObject.SetActive(false);
    }
}
