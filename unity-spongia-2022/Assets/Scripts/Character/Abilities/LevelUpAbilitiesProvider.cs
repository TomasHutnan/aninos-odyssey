using Abilities;
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

        public static SortedList<Level, AbilityName>[] GetAbilityChoices(int _level)
        {
            if (_level < 0 || _level > godAbilityLevel)
                return null;
            if (_level - 1 < maxAutoLevel)
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
                return null; // god abilities
        }
    }
}
