using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using AE.Items;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] Transform ItemSlotsGrid;
    [SerializeField] TextMeshProUGUI CurrentPageText;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<Item> OnItemClickedEvent;

    private Character c;

    private int inventoryPagesCount;
    private int currentPage = 0;

    void Start()
    {
        c = GameManager.PlayerCharacter;
        c.InventoryUpdateEvent += RefreshUI;

        for (int i = 0; i < itemSlots.Length; i++)
            itemSlots[i].OnClickEvent += OnItemClickedEvent;

        RefreshUI();
    }

    private void OnDisable()
    {
        c.InventoryUpdateEvent -= RefreshUI;
    }

    private void RefreshUI()
    {
        updatePagesCount();
        print($"inventoryPagesCount: {inventoryPagesCount}");

        CurrentPageText.text = $"Page {currentPage + 1}";

        int i = 0;
        for (; i < c.Inventory.Count - itemSlots.Length * currentPage && i < itemSlots.Length; i++)
        {
            //print(itemSlots.Length * currentPage + i);
            itemSlots[i].Item = c.Inventory[itemSlots.Length * currentPage + i];
        }
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }

    public void NextPage()
    {
        print($"inventoryPagesCount: {inventoryPagesCount}");

        if (inventoryPagesCount == 0)
            return;

        if (currentPage < inventoryPagesCount)
            currentPage += 1;
        else
            currentPage = 0;

        print($"currentPage: {currentPage}");

        RefreshUI();
    }
    public void PreviousPage()
    {
        print($"inventoryPagesCount: {inventoryPagesCount}");

        if (inventoryPagesCount == 0)
            return;

        if (currentPage > inventoryPagesCount)
            currentPage -= 1;
        else
            currentPage = inventoryPagesCount;

        print($"currentPage: {currentPage}");

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
