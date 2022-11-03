using AE.GameSave;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static AbilityStorage;

namespace AE.Abilities.UI
{
    public class AbilityLevelUpPanel : MonoBehaviour
    {
        [SerializeField] Transform AbilityDisplaysParent;
        [SerializeField] TextMeshProUGUI RemainingChoicesText;
        [SerializeField] AbilityDisplay[] AbilityDisplays;
        [Space]

        Character c = SaveData.PlayerCharacter;

        AbilityName[] choices;

        private int remainingChoices { 
            get 
            {
                int relevantLevel = c.LevelUpSystem.Level < LevelUpAbilitiesProvider.choicesMax ? c.LevelUpSystem.Level : 10;
                return relevantLevel - c.LevelUpAbilitiesCount;
            } 
        }
        
        private void OnEnable()
        {
            updateChoices();

            for (int i = 0; i < AbilityDisplays.Length; i++)
            {
                AbilityDisplays[i].OnAbilityClickedEvent += handleAbilityChoice;
            }
        }
        private void OnDisable()
        {
            for (int i = 0; i < AbilityDisplays.Length; i++)
            {
                AbilityDisplays[i].OnAbilityClickedEvent -= handleAbilityChoice;
            }
        }

        private void updateChoices()
        {
            if (remainingChoices < 1)
            {
                gameObject.SetActive(false);
                return;
            }

            updateRemainingChoicesText();

            choices = LevelUpAbilitiesProvider.GetAbilityChoices(c.LevelUpAbilitiesCount + 1);

            for (int i = 0; i < AbilityDisplays.Length; i++)
            {
                AbilityDisplays[i].AbilityName = choices[i];
            }
        }

        private void handleAbilityChoice(AbilityDisplay abilityDisplay)
        {
            AbilityName _abilityName = abilityDisplay.AbilityName;

            if (_abilityName == AbilityName.None)
            {
                print("Trying to add AbilityName.None");
                return;
            }

            c.UnlockedAbilities.Add(_abilityName);
            c.LevelUpAbilitiesCount++;

            updateChoices();
        }

        private void updateRemainingChoicesText()
        {
            RemainingChoicesText.text = $"{remainingChoices} remaining choices";
        }

        private void OnValidate()
        {
            if (AbilityDisplaysParent is not null)
                AbilityDisplays = AbilityDisplaysParent.GetComponentsInChildren<AbilityDisplay>();
        }
    }

}
