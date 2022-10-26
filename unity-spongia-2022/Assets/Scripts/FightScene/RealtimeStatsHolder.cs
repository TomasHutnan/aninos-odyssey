using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;
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
        public TextMeshProUGUI text;
        public List<ActiveEffect> activeEffects = new List<ActiveEffect>();

        public Dictionary<Stat, float> StatHolder = new Dictionary<Stat, float>();
        public Dictionary<Stat, string> StatNamer = new Dictionary<Stat, string>()
        {
            {Stat.HealthPoints,"HealthPoints" },
            {Stat.CritChance,"CritChance" },
            {Stat.Damage,"Damage" },
            {Stat.DamageReduction,"DamageReduction" },
            {Stat.DodgeChance,"DodgeChance" },
            {Stat.Mana,"Mana" },
            {Stat.Stamina,"Stamina" },
            {Stat.StaminaRegen,"StaminaRegen" },
            {Stat.Weight,"Weight" }
        };

        public GameObject Fighter;

        // Start is called before the first frame update
        void Start()
        {
            Fighter = gameObject;
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
        private void Update()
        {
            string String = "";
            foreach (Stat item in Enum.GetValues(typeof(Stat)))
            {
                String += $"{StatNamer[item]}:{StatHolder[item]}\n";
            }
            text.SetText(String);
        }

    }
}