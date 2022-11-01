using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using AE.GameSave;

public class MoneyLabel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyLabel;

    Character c;

    private void OnEnable()
    {
        if (c is null)
            c = SaveData.PlayerCharacter;

        c.MoneyUpdateEvent += updateMoneyLabel;

        updateMoneyLabel();
    }

    private void OnDisable()
    {
        c.MoneyUpdateEvent -= updateMoneyLabel;
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
