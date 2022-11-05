using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Fight
{
    public enum Location
    {
        None = -1,
        Greece,
        Rome,
        Egypt,
    }

    public enum FightType
    {
        None = -1,
        Tutorial,
        Normal,
        Boss,
    }

    public class FightData
    {
        public static Location Location;
        public static FightType FightType;
    }
}
