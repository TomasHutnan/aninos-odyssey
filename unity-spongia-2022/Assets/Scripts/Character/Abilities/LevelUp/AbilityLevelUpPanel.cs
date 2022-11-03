using Abilities;
using AE.GameSave;
using AE.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                int relevantLevel = c.LevelUpSystem.Level < LevelUpAbilitiesProvider.godAbilityLevel + 1 ? c.LevelUpSystem.Level : 10;
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

            choices = GetChoices(c.LevelUpAbilitiesCount + 1);

            int i = 0;
            for (; i < AbilityDisplays.Length && i < choices.Length; i++)
            {
                AbilityDisplays[i].AbilityName = choices[i];
            }
            for (; i < AbilityDisplays.Length; i++)
            {
                AbilityDisplays[i].AbilityName = AbilityName.None;
            }
        }

        private AbilityName[] GetChoices(int level)
        {
            print(GetAllAbilityNamesByType(AbilityTags.Attack_Ability).Length);
            List<AbilityName> _choices = new();

            SortedList<Level, AbilityName>[] loadedChoices = LevelUpAbilitiesProvider.GetAbilityChoices(level);
            SortedList<Level, AbilityName>[] shuffledChoices = RandomUtils.CreateShuffledDeck(loadedChoices).ToArray();

            int i = 0;
            while (_choices.Count < AbilityDisplays.Length && i < shuffledChoices.Length)
            {
                AbilityName abilityName = AbilityName.None;

                foreach(AbilityName _abilityName in shuffledChoices[i].Values)
                    if (!c.UnlockedAbilities.Contains(_abilityName))
                    {
                        abilityName = _abilityName;
                        break;
                    }
                if (abilityName != AbilityName.None)
                    _choices.Add(abilityName);

                i++;
            }
            return _choices.ToArray();
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
