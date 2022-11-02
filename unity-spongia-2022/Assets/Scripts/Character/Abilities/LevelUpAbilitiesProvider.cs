using Abilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AbilityStorage;

namespace AE.Abilities
{
    public class LevelUpAbilitiesProvider
    {
        private static AbilityName[][] levelUpAbilityChoices =
        {
            new AbilityName[] {AbilityName.None, AbilityName.None, AbilityName.None, AbilityName.None},
            new AbilityName[] {AbilityName.None, AbilityName.None, AbilityName.None, AbilityName.None},
            new AbilityName[] {AbilityName.None, AbilityName.None, AbilityName.None, AbilityName.None},
            new AbilityName[] {AbilityName.None, AbilityName.None, AbilityName.None, AbilityName.None},
            new AbilityName[] {AbilityName.None, AbilityName.None, AbilityName.None, AbilityName.None},
            new AbilityName[] {AbilityName.None, AbilityName.None, AbilityName.None, AbilityName.None},
            new AbilityName[] {AbilityName.None, AbilityName.None, AbilityName.None, AbilityName.None},
            new AbilityName[] {AbilityName.None, AbilityName.None, AbilityName.None, AbilityName.None},
            new AbilityName[] {AbilityName.None, AbilityName.None, AbilityName.None, AbilityName.None},
            new AbilityName[] {AbilityName.None, AbilityName.None, AbilityName.None, AbilityName.None},
        };

        public static int choicesMax { get { return levelUpAbilityChoices.Length; } }

        public static AbilityName[] GetAbilityChoices(int _level)
        {
            if (_level > 0 && _level - 1 < levelUpAbilityChoices.Length)
                return levelUpAbilityChoices[_level - 1];

            return null;
        }
    }
}
