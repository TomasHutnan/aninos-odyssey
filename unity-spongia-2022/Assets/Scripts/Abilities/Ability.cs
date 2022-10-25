using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AE.FightManager;
namespace Abilities
{
    [CreateAssetMenu]
    public class Ability : ScriptableObject
    {
        public int StaminaCost;
        public int ManaCost;
        public int Duration;
        public float TargetDamageMultiplier;
        public float SelfDamageMultiplier;

        //TargetEffects
        
        public float TargetHealthPoints;
        public float TargetCritChance;
        public float TargetDamage;
        public float TargetDamageReduction;
        public float TargetDodge;
        public float TargetStamina;
        public float TargetStaminaRegen;
        public float TargetMana;
        public float TargetWeight;
        public float TargetFinisher;
        //SelfEffects
        
        public float SelfHealthPoints; 
        public float SelfCritChance;
        public float SelfDamage;
        public float SelfDamageReduction;
        public float SelfDodge;
        public float SelfStamina;
        public float SelfStaminaRegen;
        public float SelfMana;
        public float SelfWeight;
        public float SelfFinisher;








        public void UseAbility(GameObject CasterObject, GameObject TargetObject)
        {
            Dictionary<Stat, float> TargetEffects = new Dictionary<Stat, float>()
            {
                {Stat.HealthPoints,TargetHealthPoints },
                {Stat.CritChance,TargetCritChance },
                {Stat.Damage,TargetDamage },
                {Stat.DamageReduction,TargetDamageReduction },
                {Stat.DodgeChance,TargetDodge },
                {Stat.Stamina,TargetStamina },
                {Stat.StaminaRegen,TargetStaminaRegen },
                {Stat.Mana,TargetMana },
                {Stat.Weight,TargetWeight },

            };
            Dictionary<Stat, float> SelfEffects = new Dictionary<Stat, float>()
            {
                {Stat.HealthPoints,SelfHealthPoints },
                {Stat.CritChance,SelfCritChance },
                {Stat.Damage,SelfDamage },
                {Stat.DamageReduction,SelfDamageReduction },
                {Stat.DodgeChance,SelfDodge },
                {Stat.Stamina,SelfStamina },
                {Stat.StaminaRegen,SelfStaminaRegen },
                {Stat.Mana,SelfMana },
                {Stat.Weight,SelfWeight },

            };

            var Caster = CasterObject.GetComponent<RealtimeStatsHolder>().StatHolder;
            var Target = TargetObject.GetComponent<RealtimeStatsHolder>().StatHolder;
            //Check if you can cast the spell
            float Weight = Caster[Stat.Weight];
            float Stamina = Caster[Stat.Stamina];
            float Mana = Caster[Stat.Mana];
            var OutputStaminaCost = StaminaCost + StaminaCost * Weight / 80;
            if (Stamina < OutputStaminaCost || Mana < ManaCost) { return; }
            //Stamina and Mana taking
            Caster[Stat.Mana] -= ManaCost;
            Caster[Stat.Stamina] -= OutputStaminaCost;
            //CalculatingCritChance
            float Damage = Caster[Stat.Damage];
            float CriticalChance = Caster[Stat.CritChance];
            int Chance = UnityEngine.Random.Range(1, 101);
            int CriticalMultiplier = Chance <= CriticalChance ? 2 : 1;
            //Caclulating TargetOutput Damage
            float TargetOutputDamage = (float)Math.Round(Damage * TargetDamageMultiplier) * CriticalMultiplier;
            //Calculating SelfOutput Damage
            float SelfOutputDamage = (float)Math.Round(Damage * SelfDamageMultiplier) * CriticalMultiplier;
            //All spells that dont affect Caster
            if (Target != null)
            {

                //Altering Target Deffence
                Target[Stat.DamageReduction] += TargetEffects[Stat.DamageReduction];
                //Altering Target Stamina %
                Target[Stat.Stamina] += (Target[Stat.Stamina] * TargetEffects[Stat.Stamina] / 100);
                //Altering Target CritChance 
                Target[Stat.CritChance] += TargetEffects[Stat.CritChance]; ;
                //Altering Target Damage %
                Target[Stat.Damage] += (Target[Stat.Damage] * TargetEffects[Stat.Damage] / 100);
                //Altering Target Dodge 
                Target[Stat.DodgeChance] += TargetEffects[Stat.DodgeChance]; ;
                //Altering Target Mana %
                Target[Stat.Mana] += (Target[Stat.Mana] * TargetEffects[Stat.Mana] / 100);
                //Altering Target Weight %
                Target[Stat.Weight] += (Target[Stat.Weight] * TargetEffects[Stat.Weight] / 100);
                //Altering Target Health
                float TargetHealthChange = TargetOutputDamage * (1 - Target[Stat.DamageReduction] / 100);
                Target[Stat.HealthPoints] += TargetHealthChange + TargetEffects[Stat.HealthPoints] ;
                float TargetMaxHealth = TargetObject.GetComponent<Character>().HealthPoints.Value;
                float TargetCurrentHealth = Target[Stat.HealthPoints];
                if (TargetCurrentHealth / (TargetMaxHealth / 100) <= TargetFinisher)
                {
                    Target[Stat.HealthPoints] = 0;
                }

            }
            //Altering Self Deffence
            Caster[Stat.DamageReduction] += SelfEffects[Stat.DamageReduction];
            //Altering Self Stamina %
            Caster[Stat.Stamina] += (Caster[Stat.Stamina] * SelfEffects[Stat.Stamina] / 100);
            //Altering Self CritChance 
            Caster[Stat.CritChance] += SelfEffects[Stat.CritChance]; 
            //Altering Self Self %
            Caster[Stat.Damage] += (Caster[Stat.Damage] * SelfEffects[Stat.Damage] / 100);
            //Altering Self Dodge 
            Caster[Stat.DodgeChance] += SelfEffects[Stat.DodgeChance];
            //Altering Self Mana %
            Caster[Stat.Mana] += (Caster[Stat.Mana] * SelfEffects[Stat.Mana] / 100);
            //Altering Self Weight %
            Caster[Stat.Weight] += (Caster[Stat.Weight] * SelfEffects[Stat.Weight] / 100);
            //Altering Self Health
            float SelfHealthChange = SelfOutputDamage * (1 - Caster[Stat.DamageReduction] / 100);
            Caster[Stat.HealthPoints] += SelfHealthChange + SelfEffects[Stat.HealthPoints] ;
            float SelfMaxHealth = CasterObject.GetComponent<Character>().HealthPoints.Value;
            float SelfCurrentHealth = Caster[Stat.HealthPoints];
            if (SelfCurrentHealth / (SelfMaxHealth / 100) <= SelfFinisher)
            {
                Caster[Stat.HealthPoints] = 0;
            }
            foreach (var effect in SelfEffects)
            {
                if(effect.Value != 0)
                {
                    ActiveEffect activeEffect = new ActiveEffect();
                    activeEffect.change = effect.Value;
                    activeEffect.duration = Duration;
                    activeEffect.stat = effect.Key;
                    CasterObject.GetComponent<RealtimeStatsHolder>().activeEffects.Add(activeEffect);
                }
            }
            foreach (var effect in TargetEffects)
            {
                if (effect.Value != 0)
                {
                    ActiveEffect activeEffect = new ActiveEffect();
                    activeEffect.change = effect.Value;
                    activeEffect.duration = Duration;
                    activeEffect.stat = effect.Key;
                    CasterObject.GetComponent<RealtimeStatsHolder>().activeEffects.Add(activeEffect);
                }
            }








        }


    }


}

