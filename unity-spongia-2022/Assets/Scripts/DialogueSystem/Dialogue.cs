using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Dialogue
{
    public enum Identity
    {
        None = -1,
        Anino = 100,
        You = 200,
        Analphabetic_Elephant = 300,
    }

    [CreateAssetMenu]
    public class Dialogue : ScriptableObject
    {
        [System.Serializable]
        public class Sentance
        {
            public Identity name;
            [TextArea(3, 10)]
            public string text;
        }

        public Sentance[] sentances;
    }
}
