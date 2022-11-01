using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Items
{
    public class ItemImages : MonoBehaviour
    {
        public ArmorIcon[] ArmorIcons;
        public WeaponIcon[] WeaponIcons;
        public IconBorder[] IconBorders;

        [System.Serializable]
        public class ArmorIcon
        {
            public ItemType type;
            public Sprite image;
        }
        [System.Serializable]
        public class WeaponIcon
        {
            public ItemClass _class;
            public Sprite image;
        }

        [System.Serializable]
        public class IconBorder
        {
            public ItemTier tier;
            public Sprite image;
        }
        [System.Serializable]
        public class Image
        {
            public ItemType type;
            public ItemClass _class;
            public Sprite image;
        }

        static Dictionary<ItemType, Sprite> _armorIconsDict = new();
        static Dictionary<ItemClass, Sprite> _weaponIconsDict = new();
        static Dictionary<ItemTier, Sprite> _iconBordersDict = new();

        public static Sprite GetIcon(ItemType _type, ItemClass _class)
        {
            Sprite _image;
            bool containsKey;

            if (_type == ItemType.Weapon)
                containsKey = _weaponIconsDict.TryGetValue(_class, out _image);
            else
                containsKey = _armorIconsDict.TryGetValue(_type, out _image);

            if (containsKey)
                return _image;

            return null;
        }
        public static Sprite GetIconBorder(ItemTier _tier)
        {
            Sprite _image;
            bool containsKey = _iconBordersDict.TryGetValue(_tier, out _image);

            if (containsKey)
                return _image;

            return null;
        }

        private void Awake()
        {
            foreach (ArmorIcon icon in ArmorIcons)
            {
                _armorIconsDict[icon.type] = icon.image;
            }
            foreach (WeaponIcon icon in WeaponIcons)
            {
                _weaponIconsDict[icon._class] = icon.image;
            }

            foreach (IconBorder iconBorder in IconBorders)
            {
                _iconBordersDict[iconBorder.tier] = iconBorder.image;
            }
        }
    }
}
