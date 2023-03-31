using AE.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AE.Items.UI;

public class FightFinishManager : MonoBehaviour
{
    [SerializeField] GameObject VictoryPanel;
    [SerializeField] GameObject ItemsGrid;
    [SerializeField] ItemSlot[] itemSlots;
    [SerializeField] TextMeshProUGUI MoneyText;
    [SerializeField] TextMeshProUGUI EXPText;

    [SerializeField] GameObject DefeatPanel;

    public void Victory(Item[] items, int money, int exp)
    {
        VictoryPanel.SetActive(true);

        int i = 0;
        for (; i < items.Length && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = items[i];
        }
        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }

        MoneyText.text = $"+ {money} $";
        EXPText.text = $"+ {exp} EXP";
    }
    
    public void Defeat()
    {
        DefeatPanel.SetActive(true);
    }

    private void OnValidate()
    {
        if (ItemsGrid != null)
            itemSlots = ItemsGrid.GetComponentsInChildren<ItemSlot>();
    }
}
