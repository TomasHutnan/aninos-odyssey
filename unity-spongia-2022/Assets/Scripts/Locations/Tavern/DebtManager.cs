using AE.GameSave;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebtManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI remainingDebtText;
    [Space]
    [SerializeField] TextMeshProUGUI selectedAmountText;
    [SerializeField] Scrollbar amountSelector;

    int debt;
    int maxPayableAmount { get { return debt < c.Money ? debt : c.Money; } }
    int selectedAmount = 0;

    Character c = SaveData.PlayerCharacter;

    private void OnEnable()
    {
        updateValues();
    }

    private void updateValues()
    {
        debt = SaveData.DebtRemaining;
        remainingDebtText.text = $"Remaining debt\r\n<color=#FFD700>{debt} $</color>";
        selectedAmountText.text = $"{selectedAmount} / {maxPayableAmount} $";
    }

    public void UpdateSelectedDebt(Single value)
    {
        selectedAmount = (int)Mathf.Round(value * maxPayableAmount);
        updateValues();
    }

    public void PayDebt()
    {
        if (selectedAmount > maxPayableAmount)
            selectedAmount = maxPayableAmount;

        c.Money -= selectedAmount;
        SaveData.DebtRemaining -= selectedAmount;
        amountSelector.value = 0;

        updateValues();
    }
}
