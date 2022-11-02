using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
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
