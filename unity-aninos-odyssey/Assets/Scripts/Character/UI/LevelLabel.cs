using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LevelLabel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelLabel;

    Character c;

    private void OnEnable()
    {
        if (c is null)
            c = SaveData.PlayerCharacter;

        updateLevelLabel();
    }

    private void updateLevelLabel()
    {
        levelLabel.text = c.LevelUpSystem.Level.ToString();
    }

    private void OnValidate()
    {
        if (levelLabel is null)
            levelLabel = GetComponent<TextMeshProUGUI>();
    }
}
