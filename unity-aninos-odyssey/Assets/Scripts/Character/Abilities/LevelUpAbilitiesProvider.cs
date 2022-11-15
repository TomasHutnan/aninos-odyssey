using Abilities;
using AE.Abilities.UI;
using AE.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AbilityStorage;

namespace AE.Abilities
{
    public class LevelUpAbilitiesProvider
    {
        public static int maxAutoLevel = 18;
        public static int godAbilityLevel = 19;

        public static SortedList<Level, AbilityName>[] GetAllAbilityChoices(int _level)
        {
            if (_level < 0 || _level > godAbilityLevel)
                return null;
            
            else if (_level - 1 < maxAutoLevel)
            {
                AbilityTags abilityType = AbilityTags.None;
                switch ((_level - 1) % 3)
                {
                    case 0:
                        abilityType = AbilityTags.Attack_Ability;
                        break;
                    case 1:
                        abilityType = AbilityTags.Defense_Ability;
                        break;
                    case 2:
                        abilityType = AbilityTags.Blessing_Ability;
                        break;
                }
                return GetAllAbilityNamesByType(abilityType);
            }
            else
            {
                SortedList<Level, AbilityName>[] GodAbilities =new SortedList<Level, AbilityName>[4];
                GodAbilities[0] =new SortedList < Level, AbilityName>(){ {Level.Greater, AbilityName.Earthen_Shield } };
                GodAbilities[1] = new SortedList<Level, AbilityName>() { { Level.Greater, AbilityName.BloodLust } };
                GodAbilities[2] = new SortedList<Level, AbilityName>() { { Level.Greater, AbilityName.Heavens_Blessing } };
                GodAbilities[3] = new SortedList<Level, AbilityName>() { { Level.Greater, AbilityName.Total_Precision } };
                return GodAbilities; // god abilities
            }
                
        }


        public static AbilityName[] GetChoices(Character c, int level, int choices = 4)
        {
            List<AbilityName> _choices = new();

            SortedList<Level, AbilityName>[] loadedChoices = GetAllAbilityChoices(level);
            SortedList<Level, AbilityName>[] shuffledChoices = RandomUtils.CreateShuffledDeck(loadedChoices).ToArray();

            int i = 0;
            while (_choices.Count < choices && i < shuffledChoices.Length)
            {
                AbilityName abilityName = AbilityName.None;

                foreach (AbilityName _abilityName in shuffledChoices[i].Values)
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
    }
}
