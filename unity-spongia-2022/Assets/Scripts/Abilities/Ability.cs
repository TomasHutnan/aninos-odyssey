using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu]
    public class Ability : ScriptableObject
    {
        public int StaminaCost;
        public int ManaCost;
        public float TargetDamageMultiplier;
        public float SelfDamageMultiplier;
        public float TargetFinisherDamage;

        //TargetEffects
        public float TargetHealthPoints;
        public float TargetCritChance;
        public float TargetDamage;
        public float TargetDamageReduction;
        public float TargetDoge;
        public float TargetStamina;
        public float TargetStaminaRegen;
        public float TargetMana;
        public float TargetWeight;
        //SelfEffects
        public float SelfHealthPoints;
        public float SelfCritChance;
        public float SelfDamage;
        public float SelfDamageReduction;
        public float SelfDoge;
        public float SelfStamina;
        public float SelfStaminaRegen;
        public float SelfMana;
        public float SelfWeight;







        public void UseAbility(GameObject Caster, GameObject Target)
        {

            //Check if you can cast the spell
            float Weight = Caster.GetComponent<RealtimeStatsHolder>().Weight;
            float Stamina = Caster.GetComponent<RealtimeStatsHolder>().Stamina;
            float Mana = Caster.GetComponent<RealtimeStatsHolder>().Mana;
            var OutputStaminaCost = StaminaCost + StaminaCost * Weight / 80;
            if (Stamina < OutputStaminaCost || Mana < ManaCost) { return; }
            //Stamina and Mana taking
            Caster.GetComponent<RealtimeStatsHolder>().Mana -= ManaCost;
            Caster.GetComponent<RealtimeStatsHolder>().Stamina -= OutputStaminaCost;
            //CalculatingCritChance
            float Damage = Caster.GetComponent<RealtimeStatsHolder>().Damage;
            float CriticalChance = Caster.GetComponent<RealtimeStatsHolder>().CritChance;
            int Chance = UnityEngine.Random.Range(1, 101);
            int CriticalMultiplier = Chance <= CriticalChance ? 2 : 1;
            //Caclulating TargetOutput Damage
            float TargetOutputDamage = (float)Math.Round(Damage * TargetDamageMultiplier) * CriticalMultiplier;
            //Calculating SelfOutput Damage
            float SelfOutputDamage = (float)Math.Round(Damage * SelfDamageMultiplier) * CriticalMultiplier;
            //All spells that dont affect Caster
            if (Target != null)
            {
                RealtimeStatsHolder TargetComponent = Target.GetComponent<RealtimeStatsHolder>();
                //Altering Target Deffence

                TargetComponent.DamageReduction += TargetDamageReduction;
                //Altering Target Stamina %
                TargetComponent.Stamina += (Target.GetComponent<RealtimeStatsHolder>().Stamina * TargetStamina / 100);
                //Altering Target CritChance 
                TargetComponent.CritChance -= TargetCritChance;
                //Altering Target Damage %
                TargetComponent.Damage += (Target.GetComponent<RealtimeStatsHolder>().Damage * TargetDamage / 100);
                //Altering Target Doge 
                TargetComponent.Doge += TargetDoge;
                //Altering Target Mana %
                TargetComponent.Mana += (Target.GetComponent<RealtimeStatsHolder>().Mana * TargetMana / 100);
                //Altering Target Weight %
                TargetComponent.Weight += (Target.GetComponent<RealtimeStatsHolder>().Weight * TargetWeight / 100);
                //AlteringTargetHealth
                float TargetHealthChange = TargetOutputDamage * (1 - Target.GetComponent<RealtimeStatsHolder>().DamageReduction / 100);
                TargetComponent.HealthPoints += TargetHealthChange + TargetHealthPoints;

            }
            RealtimeStatsHolder CasterComponent = Caster.GetComponent<RealtimeStatsHolder>();
            //Altering Self Deffence
            CasterComponent.DamageReduction += SelfDamageReduction;
            //Altering Self Stamina %
            CasterComponent.Stamina += (Caster.GetComponent<RealtimeStatsHolder>().Stamina * SelfStamina / 100);
            //Altering Self CritChance 
            CasterComponent.CritChance -= SelfCritChance;
            //Altering Self Damage %
            CasterComponent.Damage += (Caster.GetComponent<RealtimeStatsHolder>().Damage * SelfDamage / 100);
            //Altering Self Doge 
            CasterComponent.Doge += SelfDoge;
            //Altering Self Mana %
            CasterComponent.Mana += (Caster.GetComponent<RealtimeStatsHolder>().Mana * SelfMana / 100);
            //Altering Self Weight %
            CasterComponent.Weight += (Caster.GetComponent<RealtimeStatsHolder>().Weight * SelfWeight / 100);
            //Altering SelfHealth
            float SelfHealthChange = SelfOutputDamage * (1 - Caster.GetComponent<RealtimeStatsHolder>().DamageReduction / 100);
            CasterComponent.HealthPoints += SelfHealthChange + SelfHealthPoints;






        }


    }


}

