using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
public class AbilityStorage : MonoBehaviour
{
    public enum AbilityName
    {
        None,

    }
    [System.Serializable]
    public class StoredAbility
    {
       
        public AbilityName Name;
        public Ability ability;
        
    }
    public List<StoredAbility> StoredAbilities = new List<StoredAbility>();
    public static Dictionary<AbilityName,Ability> GetAbility = new Dictionary<AbilityName,Ability>();
    private void Awake()
    {
        GetAbility[AbilityName.None] = null;
        foreach (var item in StoredAbilities)
        {
            GetAbility[item.Name] = item.ability;
        }
    }
    // Start is called before the first frame update

}
