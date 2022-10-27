using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class MoneyLabel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyLabel;

    Character c;

    private void Start()
    {
        if (c is null)
            c = GameManager.PlayerCharacter;

        c.InventoryUpdateEvent += updateMoneyLabel;
        updateMoneyLabel();
    }

    private void OnDisable()
    {
        c.InventoryUpdateEvent -= updateMoneyLabel;
    }

    private void updateMoneyLabel()
    {
        moneyLabel.text = $"{c.Money} $";
    }

    private void OnValidate()
    {
        if (moneyLabel is null)
            moneyLabel = GetComponent<TextMeshProUGUI>();
    }
}
