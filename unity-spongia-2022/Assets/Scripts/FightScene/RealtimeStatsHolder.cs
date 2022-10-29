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
        public List<ActiveEffect> delayedEffects = new List<ActiveEffect>();
        public Dictionary<Stat, List<ActiveEffect>> activeEffects =new Dictionary<Stat, List<ActiveEffect>>()
        {
            {Stat.HealthPoints, new List<ActiveEffect>()},
            {Stat.CritChance, new List<ActiveEffect>()},
            {Stat.Damage, new List<ActiveEffect>()},
            {Stat.DamageReduction, new List<ActiveEffect>()},
            {Stat.DodgeChance, new List<ActiveEffect>()},
            {Stat.Stamina, new List<ActiveEffect>()},
            {Stat.StaminaRegen, new List<ActiveEffect>()},
            {Stat.Mana, new List<ActiveEffect>()},
            {Stat.Weight, new List<ActiveEffect>()},
        };

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
        public void NextRound()
        {
            List<ActiveEffect> ToDelete = new List<ActiveEffect>();
            foreach (var item in activeEffects)
            {
                foreach (var item2 in item.Value)
                {
                    item2.duration -= 1;
                    if (item2.duration == 0)
                    {
                        StatHolder[item.Key] += item2.change * -1;
                        ToDelete.Add(item2);
                    }
                    

                }
                foreach (var delete in ToDelete)
                {
                    item.Value.Remove(delete);

                }
                ToDelete.Clear();

            }


            foreach (var item in delayedEffects)
            {
                item.delay -= 1;
                if (item.delay == 0)
                {
                    activeEffects[item.stat].Add(item);
                    ToDelete.Add(item);
                }
               

            }
            foreach (var delete in ToDelete)
            {
                delayedEffects.Remove(delete);

            }

        }
        public void ChangeInStats(Stat StatToChange,float ChangeValue)
        {
            StatHolder[StatToChange] += ChangeValue;
            List<ActiveEffect> ToDelete = new List<ActiveEffect>();
            foreach (var item in activeEffects[StatToChange])
            {
                if(item.type == StatType.Percentual) { continue; };
                //Check if sign of our current change is the opposite of change in active effects buffer
                if(item.change/Math.Abs(item.change)*-1 == ChangeValue / Math.Abs(ChangeValue))
                {
                    item.change += ChangeValue;
                    if(item.change <= 0)
                    {
                        ChangeValue = item.change*-1;
                        ToDelete.Add(item);
                        
                    }
                    else
                    {
                        ChangeValue = 0;
                        break;
                    }
                }

            }
            foreach (var item in ToDelete)
            {
                activeEffects[StatToChange].Remove(item);
            }
        }
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
            foreach (var item in delayedEffects)
            {
                if(item.delay != 0) { continue; };
                ChangeInStats(item.stat, item.change);
                if(item.duration != 0) 
                {
                    activeEffects[item.stat].Add(item);
                };
                //delayedEffects.Remove(item);
            }



            //print(activeEffects.Count);
            string String = "";
            foreach (Stat item in Enum.GetValues(typeof(Stat)))
            {
                String += $"{StatNamer[item]}:{StatHolder[item]}\n";
            }
            text.SetText(String);
        }

    }
}