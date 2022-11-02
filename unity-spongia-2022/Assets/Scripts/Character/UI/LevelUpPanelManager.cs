using AE.GameSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.CharacterStats.UI
{
    public class LevelUpPanelManager : MonoBehaviour
    {
        [SerializeField] GameObject[] LevelUpPanels;
        [SerializeField] Character c = SaveData.PlayerCharacter;

        void Start()
        {
            if (c.LevelUpSystem.UnspentSkillPoints > 0)
                foreach (GameObject panel in LevelUpPanels)
                    panel.SetActive(true);
        }
    }
}
