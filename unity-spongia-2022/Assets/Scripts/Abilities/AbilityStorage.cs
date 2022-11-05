using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
using System.Linq;

public class AbilityStorage : MonoBehaviour
{
    public enum AbilityName
    {
        None,
        //Attacks
        Armor_Break,
        Fatigue,
        Fighters_Attack,
        Greater_Armor_Break,
        Greater_Fatigue,
        Greater_Fighters_Attack,
        Greater_Priest_Attack,
        Greater_Rouge_Attack,
        Greater_Tank_Attack,
        Lesser_Attack,
        Priest_Attack,
        Rouge_Attack,
        Stun_Attack,
        Tank_Attack,
        //Blessing
        Greater_Crit_Chance_Blessing,
        Greater_Damage_Blessing,
        Greater_Defence_Blessing,
        Greater_Heal_Blessing,
        //Greater_Dodge_Chance_Blessing,
        //Greater_Health_Blessing,
        Lesser_Crit_Chance_Blessing,
        Lesser_Damage_Blessing,
        Lesser_Defence_Blessing,
        Lesser_Dodge_Chance_Blessing,
        Lesser_Heal_Blessing,
        Crit_Chance_Blessing,
        Damage_Blessing,
        Defence_Blessing,
        Dodge_Chance_Blessing,
        Heal_Blessing,
        //DefensiveBlessing
        Greater_Fighters_Defence,
        Greater_Priests_Defence,
        Greater_Rouges_Defence,
        Greater_Tanks_Defence,
        Fighters_Defence,
        Priests_Defence,
        Rouges_Defence,
        Tanks_Defence,
        Lesser_Defence,
        //GodAbilities
        BloodLust,
        Earthen_Shield,
        Heavens_Blessing,
        Total_Precision,
        Greater_Dodge_Chance_Blessing,
        







    }
    [System.Serializable]
    public class StoredAbility
    {
       
        public AbilityName Name;
        public Ability ability;
        
    }
    public List<StoredAbility> StoredAbilities = new List<StoredAbility>();
    public static Dictionary<AbilityName,Ability> GetAbility = new Dictionary<AbilityName,Ability>();

    static Dictionary<AbilityTags, Dictionary<Family, SortedList<Level, AbilityName>>> abilityFamilyTypes = new Dictionary<AbilityTags, Dictionary<Family, SortedList<Level, AbilityName>>>
    {
        {AbilityTags.Attack_Ability, new Dictionary<Family, SortedList<Level, AbilityName>> { } },
        {AbilityTags.Defense_Ability, new Dictionary<Family, SortedList<Level, AbilityName>> { } },
        {AbilityTags.Blessing_Ability, new Dictionary<Family, SortedList<Level, AbilityName>> { } },
    };

    private void Awake()
    {
        GetAbility[AbilityName.None] = null;
        foreach (StoredAbility item in StoredAbilities)
        {
            AbilityName abilityName = item.Name;
            Ability ability = item.ability;

            GetAbility[abilityName] = ability;

            if (ability.AbilityType != AbilityTags.None)
            {
                addAbilityToFamilyDict(abilityName, ability);
            }
        }
    }
    // Start is called before the first frame update

    public static SortedList<Level, AbilityName>[] GetAllAbilityNamesByType(AbilityTags abilityType)
    {
        Dictionary<Family, SortedList<Level, AbilityName>> outVal;
        bool keyExists = abilityFamilyTypes.TryGetValue(abilityType, out outVal);
        if (keyExists)
            return outVal.Values.ToArray();

        return null;
    }

    private void addAbilityToFamilyDict(AbilityName abilityName, Ability ability)
    {
        abilityFamilyTypes.TryAdd(ability.AbilityType, new Dictionary<Family, SortedList<Level, AbilityName>>());
        abilityFamilyTypes[ability.AbilityType].TryAdd(ability.AbilityFamily, new SortedList<Level, AbilityName>());

        if (ability.AbilityLevel != Level.None && !abilityFamilyTypes[ability.AbilityType][ability.AbilityFamily].Contains(new KeyValuePair<Level, AbilityName>(ability.AbilityLevel, abilityName)))
            abilityFamilyTypes[ability.AbilityType][ability.AbilityFamily].Add(key: ability.AbilityLevel, value: abilityName);
    }
}
