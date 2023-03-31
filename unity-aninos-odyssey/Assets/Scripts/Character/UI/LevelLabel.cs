using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelLabel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelLabel;
    [SerializeField] Slider EXPSlider;

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

        if (c.LevelUpSystem.Level == 20)
            EXPSlider.gameObject.SetActive(false);
        else
            EXPSlider.value = (float)(((float)c.LevelUpSystem.LevelUpEXP - (float)c.LevelUpSystem.Exp) / (float)c.LevelUpSystem.LevelUpEXP);
    }

    private void OnValidate()
    {
        if (levelLabel is null)
            levelLabel = GetComponent<TextMeshProUGUI>();
    }
}
