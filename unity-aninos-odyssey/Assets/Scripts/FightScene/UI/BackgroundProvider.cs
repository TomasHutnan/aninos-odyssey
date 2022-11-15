using AE.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundProvider : MonoBehaviour
{
    [System.Serializable]
    public class BackgroundInfo
    {
        public Sprite image;
        public Location FightLocation;
        public FightType FightType;
    }

    [SerializeField] Image background;

    [SerializeField] BackgroundInfo[] backgroundInfos;

    private Dictionary<Location, Dictionary<FightType, Sprite>> _backgrounds = new();


    private void Awake()
    {
        foreach (BackgroundInfo info in backgroundInfos)
        {
            if (!_backgrounds.ContainsKey(info.FightLocation))
                _backgrounds[info.FightLocation] = new Dictionary<FightType, Sprite>();

            _backgrounds[info.FightLocation][info.FightType] = info.image;
        }

        if (FightData.FightType == FightType.None)
            FightData.FightType = FightType.Normal;
        if (FightData.Location == Location.None)
            FightData.Location = Location.Greece;

        background.sprite = _backgrounds[FightData.Location][FightData.FightType];
    }
}
