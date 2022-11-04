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

    public class FightData
    {
        public static Location Location;
        public static bool IsBoss;
    }
}
