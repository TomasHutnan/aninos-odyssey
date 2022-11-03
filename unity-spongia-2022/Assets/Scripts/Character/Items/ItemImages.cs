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

        public ArmorImage[] ArmorImages;
        public WeaponImage[] WeaponImages;

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
        public class ArmorImage
        {
            public ItemTier tier;
            public ItemType type;
            public Sprite image;
        }
        [System.Serializable]
        public class WeaponImage
        {
            public ItemTier tier;
            public ItemClass _class;
            public Sprite image;
        }

        static Dictionary<ItemType, Sprite> _armorIconsDict = new();
        static Dictionary<ItemClass, Sprite> _weaponIconsDict = new();
        static Dictionary<ItemTier, Sprite> _iconBordersDict = new();

        static Dictionary<ItemTier, Dictionary<ItemType, Sprite>> _armorImagesDict = new Dictionary<ItemTier, Dictionary<ItemType, Sprite>> 
        {
            {ItemTier.Mortal, new Dictionary<ItemType, Sprite>() },
            {ItemTier.Earth, new Dictionary<ItemType, Sprite>() },
            {ItemTier.Heaven, new Dictionary<ItemType, Sprite>() },
            {ItemTier.God, new Dictionary<ItemType, Sprite>() },
        };
        static Dictionary<ItemTier, Dictionary<ItemClass, Sprite>> _weaponImagesDict = new Dictionary<ItemTier, Dictionary<ItemClass, Sprite>>
        {
            {ItemTier.Mortal, new Dictionary<ItemClass, Sprite>() },
            {ItemTier.Earth, new Dictionary<ItemClass, Sprite>() },
            {ItemTier.Heaven, new Dictionary<ItemClass, Sprite>() },
            {ItemTier.God, new Dictionary<ItemClass, Sprite>() },
        };

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

        public static Sprite GetImage(ItemTier _tier, ItemType _type, ItemClass _class)
        {
            if (_type == ItemType.Weapon)
                if (_weaponImagesDict.ContainsKey(_tier))
                {
                    Sprite _image;
                    bool containsKey = _weaponImagesDict[_tier].TryGetValue(_class, out _image);
                    if (containsKey)
                        return _image;
                }
            else
                if (_armorImagesDict.ContainsKey(_tier))
                {
                    Sprite _image;
                    bool containsKey = _armorImagesDict[_tier].TryGetValue(_type, out _image);
                    if (containsKey)
                        return _image;
                }

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

            foreach(ArmorImage image in ArmorImages)
            {
                _armorImagesDict[image.tier][image.type] = image.image;
            }
            foreach (WeaponImage image in WeaponImages)
            {
                _weaponImagesDict[image.tier][image._class] = image.image;
            }
        }
    }
}
