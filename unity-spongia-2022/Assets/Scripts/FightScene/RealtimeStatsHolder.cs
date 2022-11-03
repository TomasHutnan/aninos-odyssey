using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;
using Abilities;
using Newtonsoft.Json.Linq;
using static AbilityStorage;
using System.Linq;

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
        public void Print(string msg)
        {
            print(msg);
        }

        public AbilityName[] AvailableAbilities;
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
        public Character _fighter;
        public void SetCharacter(Character character)
        {
           
            StatHolder.Add(Stat.HealthPoints, character.HealthPoints.Value);
          
            StatHolder.Add(Stat.CritChance, character.CritChance.Value);
            StatHolder.Add(Stat.Damage, character.Damage.Value);
            StatHolder.Add(Stat.DamageReduction, character.DamageReduction.Value);
            StatHolder.Add(Stat.DodgeChance, character.DodgeChance.Value);
            StatHolder.Add(Stat.Stamina, character.Stamina.Value);
            StatHolder.Add(Stat.StaminaRegen, 1);
            StatHolder.Add(Stat.Mana, character.Mana.Value);
            StatHolder.Add(Stat.Weight, character.Weight.Value);
            StatHolder.Add(Stat.Stun, 0);
            AvailableAbilities = character.EquippedAbilities.ToArray();
            _fighter = character;
        }
       
        public void NextRound()
        {
            Update();
            StatHolder[Stat.Stamina] += _fighter.Stamina.Value * 0.25f;
            StatHolder[Stat.Mana] += _fighter.Mana.Value *0.1f;

            print("NextRound");
            List<ActiveEffect> ToDelete = new List<ActiveEffect>();
            AvailableAbilities = _fighter.EquippedAbilities.ToArray(); ;



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
            
           


        }

        // Update is called once per frame
        private void Update()
        {
            List<ActiveEffect> ToDelete = new List<ActiveEffect>();
            //print(delayedEffects.Count);
            foreach (var item in delayedEffects)
            {
                print(item.delay);
                if (item.delay != 0) { continue; };
                
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