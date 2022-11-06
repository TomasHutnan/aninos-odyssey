using AE.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AE.Fight.UI
{
    public enum StanceType
    {
        None = -1,
        Idle,
        Attack,
        Defend,
    }

    public enum AnimationState
    {
        None = -1,
        Idle,
        Animating,
    }

    public enum AnimationWeaponClass
    {
        None = -1,
        NoWeapon,
        Fighter,
        Tank,
        Rogue,
        Pries,
    }

    public class StanceController : MonoBehaviour
    {
        [SerializeField]
        Transform[] weaponStances;

        private Dictionary<AnimationWeaponClass, Dictionary<StanceType, Image[]>> _stanceImages = new Dictionary<AnimationWeaponClass, Dictionary<StanceType, Image[]>>();

        private AnimationWeaponClass weaponType;

        public Character character 
        { 
            set 
            {
                Item weapon;
                bool contains = value.EquippedItems.TryGetValue(ItemType.Weapon, out weapon);

                if (contains)
                    weaponType = (AnimationWeaponClass)((int)weapon.Class + 1);
                else
                    weaponType = AnimationWeaponClass.NoWeapon;

                updateClass();
            } 
        }

        [Header("Animation")]
        public int AnimationLength;

        public void Animate(StanceType stanceType)
        {

        }

        private void Awake()
        {
            for (int _class = 0; _class < weaponStances.Length; _class++)
            {
                _stanceImages[(AnimationWeaponClass)_class] = new Dictionary<StanceType, Image[]>();
                for (int _stanceType = 0; _stanceType < 3; _stanceType++)
                {
                    _stanceImages[(AnimationWeaponClass)_class][(StanceType)_stanceType] = new Image[weaponStances[_class].GetChild(_stanceType).childCount];
                    for (int childIndex = 0; childIndex < _stanceImages[(AnimationWeaponClass)_class][(StanceType)_stanceType].Length; childIndex++)
                    {
                        _stanceImages[(AnimationWeaponClass)_class][(StanceType)_stanceType][childIndex] = weaponStances[_class].GetChild(_stanceType).GetChild(childIndex).GetComponent<Image>();
                    }
                }
            }
        }

        private void updateClass()
        {
            StopAllCoroutines();

        }

        private void fadeGroups(Image[] fadeInGroup, Image[] fadeOutGroup)
        {
            foreach (Image image in fadeInGroup)
            {
                StartCoroutine(FadeIn(image));
            }
            foreach (Image image in fadeOutGroup)
            {
                StartCoroutine(FadeOut(image));
            }
        }

        private IEnumerator FadeIn(Image image)
        {
            for (float i = 0; i <= AnimationLength; i += Time.deltaTime)
            {
                image.color = new Color(1, 1, 1, i / AnimationLength);
                yield return null;
            }
        }
        private IEnumerator FadeOut(Image image)
        {
            for (float i = AnimationLength; i >= 0; i -= Time.deltaTime)
            {
                image.color = new Color(1, 1, 1, i / AnimationLength);
                yield return null;
            }
        }

        private void OnValidate()
        {
            weaponStances = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
                weaponStances[i] = transform.GetChild(i);
        }
    }
}
