using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
public class AbilityStorage : MonoBehaviour
{
    public enum AbilityName
    {

    }
    [System.Serializable]
    public class StoredAbility
    {
       
        public AbilityName Name;
        public Ability ability;
        
    }
    // Start is called before the first frame update
    public List<StoredAbility> StoredAbilities = new List<StoredAbility>();
}
