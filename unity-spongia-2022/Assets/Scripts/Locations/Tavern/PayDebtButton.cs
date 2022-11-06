using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayDebtButton : MonoBehaviour
{
    private void OnEnable()
    {
        if (SaveData.DebtRemaining <= 0)
            gameObject.SetActive(false);
    }
}
