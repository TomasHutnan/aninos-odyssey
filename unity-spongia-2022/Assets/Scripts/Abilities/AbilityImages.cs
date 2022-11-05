using Abilities;
using AE.FightManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AbilityStorage;

namespace AE.Abilities
{
    public enum AbilityBuffType
    {
        None = -1,
        Buff = 0,
        Debuff = 1,
    }
    public class AbilityImages : MonoBehaviour
    {
        [System.Serializable]
        public class ForegroundImage
        {
            public Sprite Image;
            public Level Level;
            public AbilityBuffType BuffType;
        }

        public Sprite AbilityBackground;

        public ForegroundImage[] ForegroundImages;

        static Dictionary<Level, Sprite> _levelToForeground = new();
        static Dictionary<AbilityBuffType, Sprite> _buffTypeToForeground = new();
        static Sprite _bg;

        private void Awake()
        {
            _bg = AbilityBackground;
            foreach (ForegroundImage image in ForegroundImages)
            {
                if (image.Level != Level.None)
                    _levelToForeground[image.Level] = image.Image;

                if (image.BuffType != AbilityBuffType.None)
                    _buffTypeToForeground[image.BuffType] = image.Image;
            }
        }

        public static Sprite GetLevelFG(AbilityName abilityName)
        {
            Sprite image;
            bool contains = _levelToForeground.TryGetValue(GetAbility[abilityName].AbilityLevel, out image);
            if (contains)
                return image;

            return null;
        }
        public static Sprite GetBuffFG(Stat statType, float valueChange)
        {
            AbilityBuffType key;
            if (valueChange < 0 || statType == Stat.Stun)
                key = AbilityBuffType.Debuff;
            else
                key = AbilityBuffType.Buff;

            Sprite image;
            bool contains = _buffTypeToForeground.TryGetValue(key, out image);
            if (contains)
                return image;

            return null;
        }

        public static Sprite GetBG()
        {
            return _bg;
        }
    }
}
