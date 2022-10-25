using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace AE.FightManager
{
    public enum Stat
    {
        HealthPoints,
        CritChance,
        Damage,
        DamageReduction,
        DodgeChance,
        Stamina,
        StaminaRegen,
        Mana,
        Weight,

    }
    
    public class RealtimeStatsHolder : MonoBehaviour
    {
        public List<ActiveEffect> activeEffects = new List<ActiveEffect>();

        public Dictionary<Stat, float> StatHolder = new Dictionary<Stat, float>();

        public GameObject Fighter;

        // Start is called before the first frame update
        void Start()
        {
            StatHolder.Add(Stat.HealthPoints, Fighter.GetComponent<Character>().HealthPoints.Value);
            StatHolder.Add(Stat.CritChance, Fighter.GetComponent<Character>().CritChance.Value);
            StatHolder.Add(Stat.Damage, Fighter.GetComponent<Character>().Damage.Value);
            StatHolder.Add(Stat.DamageReduction, Fighter.GetComponent<Character>().DamageReduction.Value);
            StatHolder.Add(Stat.DodgeChance, Fighter.GetComponent<Character>().DodgeChance.Value);
            StatHolder.Add(Stat.Stamina, Fighter.GetComponent<Character>().Stamina.Value);
            StatHolder.Add(Stat.StaminaRegen, Fighter.GetComponent<Character>().StaminaRegen.Value);
            StatHolder.Add(Stat.Mana, Fighter.GetComponent<Character>().Mana.Value);
            StatHolder.Add(Stat.Weight, Fighter.GetComponent<Character>().Weight.Value);



        }

        // Update is called once per frame

    }
}