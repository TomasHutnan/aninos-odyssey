using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Dialogue
{
    [CreateAssetMenu]
    public class Dialogue : ScriptableObject
    {
        [System.Serializable]
        public class Sentance
        {
            public string name;
            [TextArea(3, 10)]
            public string text;
        }

        public Sentance[] sentances;
    }
}
