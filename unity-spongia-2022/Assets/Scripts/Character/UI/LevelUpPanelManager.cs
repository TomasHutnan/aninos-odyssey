using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.CharacterStats.UI
{
    public class LevelUpPanelManager : MonoBehaviour
    {
        [SerializeField] GameObject LevelUpPanel;
        [SerializeField] Character c;

        void Start()
        {
            if (c is null)
                c = SaveData.PlayerCharacter;

            if (c.LevelUpSystem.UnspentSkillPoints > 0)
                LevelUpPanel.SetActive(true);
        }
    }
}
