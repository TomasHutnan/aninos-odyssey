using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AE.GameSave;
using AE.Abilities.UI;
using static AbilityStorage;

namespace AE.CharacterStats.UI
{
    public class LevelUpPanel : MonoBehaviour
    {
        [SerializeField] Transform LevelUpDisplaysParent;
        [SerializeField] TextMeshProUGUI unspentPointsText;
        [SerializeField] LevelUpDisplay[] levelUpDisplays;
        [Space]

        [SerializeField] Character c = SaveData.PlayerCharacter;

        private void Start()
        {
            LevelUpModType[] modTypes = (LevelUpModType[])Enum.GetValues(typeof(LevelUpModType));
            for (int i = 0; i < modTypes.Length; i++)
            {
                levelUpDisplays[i].ModType = modTypes[i];
            }
        }

        private void OnEnable()
        {
            if (c.LevelUpSystem.UnspentSkillPoints <= 0)
                gameObject.SetActive(false);

            updateUnspentPointsText();

            for (int i = 0; i < Enum.GetValues(typeof(LevelUpModType)).Length; i++)
            {
                levelUpDisplays[i].LevelUpSelectEvent += handleLevelUp;
            }
        }
        private void OnDisable()
        {
            for (int i = 0; i < Enum.GetValues(typeof(LevelUpModType)).Length; i++)
            {
                levelUpDisplays[i].LevelUpSelectEvent -= handleLevelUp;
            }
        }

        private void handleLevelUp(LevelUpModType modType)
        {
            c.LevelUpSystem.LevelUp(modType);
            updateUnspentPointsText();

            if (c.LevelUpSystem.UnspentSkillPoints <= 0)
                gameObject.SetActive(false);
        }

        private void updateUnspentPointsText()
        {
            unspentPointsText.text = $"{c.LevelUpSystem.UnspentSkillPoints} Unspent Skill Points";
        }

        private void OnValidate()
        {
            if (LevelUpDisplaysParent is not null)
                levelUpDisplays = LevelUpDisplaysParent.GetComponentsInChildren<LevelUpDisplay>();
        }
    }
}
