using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;
using Abilities;

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
        Stun,

    }
   
    public class RealtimeStatsHolder : MonoBehaviour
    {

        public List<Ability> AvailableAbilities = new List<Ability>();
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
            {Stat.Stun, new List<ActiveEffect>()},
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
            {Stat.Weight,"Weight" },
            {Stat.Stun,"Stun" }
        };

        public Character Fighter;
        public void NextRound()
        {
            print("NextRound");
            List<ActiveEffect> ToDelete = new List<ActiveEffect>();
            AvailableAbilities = Fighter.EquipedAbilities;



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
                    ChangeInStats(item.stat, item.change);
                    ToDelete.Add(item);
                    if (item.duration != 0)
                    {
                        activeEffects[item.stat].Add(item);
                    }
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
                if(item.type == StatType.Percentual || item.type == StatType.Stun) { continue; };
                //Check if sign of our current change is the opposite of change in active effects buffer
                if(item.change/Math.Abs(item.change)*-1 == ChangeValue / Math.Abs(ChangeValue))
                {
                    print($"Detected,{item.change},{ChangeValue}");
                    item.change += ChangeValue;
                    //BULLSHIT
                    if((item.change / Math.Abs(item.change) )* item.change <= 0)
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
            Fighter = gameObject.GetComponent<Character>();
            StatHolder.Add(Stat.HealthPoints, Fighter.HealthPoints.Value);
            StatHolder.Add(Stat.CritChance, Fighter.CritChance.Value);
            StatHolder.Add(Stat.Damage, Fighter.Damage.Value);
            StatHolder.Add(Stat.DamageReduction, Fighter.DamageReduction.Value);
            StatHolder.Add(Stat.DodgeChance, Fighter.DodgeChance.Value);
            StatHolder.Add(Stat.Stamina, Fighter.Stamina.Value);
            StatHolder.Add(Stat.StaminaRegen, Fighter.StaminaRegen.Value);
            StatHolder.Add(Stat.Mana, Fighter.Mana.Value);
            StatHolder.Add(Stat.Weight, Fighter.Weight.Value);
            StatHolder.Add(Stat.Stun, 0);
            AvailableAbilities = Fighter.EquipedAbilities;


        }

        // Update is called once per frame
        private void Update()
        {
            List<ActiveEffect> ToDelete = new List<ActiveEffect>();
            print($"DelayedEffectLength{delayedEffects.Count}");
            foreach (var item in delayedEffects)
            {
                if(item.delay != 0) { continue; };
                ChangeInStats(item.stat, item.change);
                if(item.duration != 0) 
                {
                    activeEffects[item.stat].Add(item);
                };
                ToDelete.Add(item); 
            }

            foreach (var item in ToDelete)
            {
                delayedEffects.Remove(item);
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