using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Utils
{
    public class EnumUtils
    {
        public static T RandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(UnityEngine.Random.Range(0, v.Length));
        }
    }
}
