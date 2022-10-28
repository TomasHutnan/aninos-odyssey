using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AE.FightManager;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Abilities
{
    [CreateAssetMenu]
    public class Ability : ScriptableObject
    {
        public int StaminaCost;
        public int ManaCost;
        public float TargetDamageMultiplier;
        public float SelfDamageMultiplier;
        public bool AffectsTarget;
        public bool AffectsCaster;

        //TargetEffects

        [System.NonSerialized] public float TargetHealthPoints;
        float TargetHealthDuration;
        float TargetHealthDelay;

        [System.NonSerialized] public float TargetCritChance;
        float TargetCritDuration;
        float TargetCritDelay;

        [System.NonSerialized] public float TargetDamage;
        float TargetDamageDuration;
        float TargetDamageDelay;


        [System.NonSerialized] public float TargetDamageReduction;
        float TargetDamageReductionDuration;
        float TargetDamageReductionDelay;


        [System.NonSerialized] public float TargetDodge;
        float TargetDodgeDuration;
        float TargetDodgeDelay;


        [System.NonSerialized] public float TargetStamina;
        float TargetStaminaDuration;
        float TargetStaminaDelay;


        [System.NonSerialized] public float TargetStaminaRegen;
        float TargetStaminaRegenDuration;
        float TargetStaminaRegenDelay;


        [System.NonSerialized] public float TargetMana;
        float TargetManaDuration;
        float TargetManaDelay;


        [System.NonSerialized] public float TargetWeight;
        float TargetWeightDuration;
        float TargetWeightDelay;


        [System.NonSerialized] public float TargetFinisher;



        //SelfEffects

        [System.NonSerialized] public float SelfHealthPoints;
        float SelfHealthDuration;
        float SelfHealthDelay;

        [System.NonSerialized] public float SelfCritChance;
        float SelfCritDuration;
        float SelfCritDelay;

        [System.NonSerialized] public float SelfDamage;
        float SelfDamageDuration;
        float SelfDamageDelay;


        [System.NonSerialized] public float SelfDamageReduction;
        float SelfDamageReductionDuration;
        float SelfDamageReductionDelay;


        [System.NonSerialized] public float SelfDodge;
        float SelfDodgeDuration;
        float SelfDodgeDelay;

        [System.NonSerialized] public float SelfStamina;
        float SelfStaminaDuration;
        float SelfStaminaDelay;

        [System.NonSerialized] public float SelfStaminaRegen;
        float SelfStaminaRegenDuration;
        float SelfStaminaRegenDelay;

        [System.NonSerialized] public float SelfMana;
         float SelfManaDuration;
         float SelfManaDelay;



        [System.NonSerialized] public float SelfWeight;
         float SelfWeightDuration;
         float SelfWeightDelay;


        [System.NonSerialized] public float SelfFinisher;




     






        public void UseAbility(GameObject CasterObject, GameObject TargetObject)
        {
            Dictionary<Stat, float> SelfDurations = new Dictionary<Stat, float>()
            {
                {Stat.HealthPoints,SelfHealthDuration },
                {Stat.CritChance,SelfCritDuration },
                {Stat.Damage,SelfDamageDuration },
                {Stat.DamageReduction,SelfDamageReductionDuration },
                {Stat.DodgeChance,SelfDodgeDuration },
                {Stat.Stamina,SelfStaminaDuration },
                {Stat.StaminaRegen,SelfStaminaRegenDuration },
                {Stat.Mana,SelfManaDuration },
                {Stat.Weight,SelfWeightDuration },

            };

            Dictionary<Stat, float> SelfDelays = new Dictionary<Stat, float>()
            {
                {Stat.HealthPoints,SelfHealthDelay },
                {Stat.CritChance,SelfCritDelay },
                {Stat.Damage,SelfDamageDelay },
                {Stat.DamageReduction,SelfDamageReductionDelay },
                {Stat.DodgeChance,SelfDodgeDelay },
                {Stat.Stamina,SelfStaminaDelay },
                {Stat.StaminaRegen,SelfStaminaRegenDelay },
                {Stat.Mana,SelfManaDelay },
                {Stat.Weight,SelfWeightDelay },

            };

            Dictionary<Stat, float> TargetDelays = new Dictionary<Stat, float>()
            {
                {Stat.HealthPoints,TargetHealthDelay },
                {Stat.CritChance,TargetCritDelay },
                {Stat.Damage,TargetDamageDelay },
                {Stat.DamageReduction,TargetDamageReductionDelay },
                {Stat.DodgeChance,TargetDodgeDelay },
                {Stat.Stamina,TargetStaminaDelay },
                {Stat.StaminaRegen,TargetStaminaRegenDelay },
                {Stat.Mana,TargetManaDelay },
                {Stat.Weight,TargetWeightDelay },

            };

            Dictionary<Stat, float> TargetDurations = new Dictionary<Stat, float>()
            {
                {Stat.HealthPoints,TargetHealthDuration },
                {Stat.CritChance,TargetCritDuration },
                {Stat.Damage,TargetDamageDuration },
                {Stat.DamageReduction,TargetDamageReductionDuration },
                {Stat.DodgeChance,TargetDodgeDuration },
                {Stat.Stamina,TargetStaminaDuration },
                {Stat.StaminaRegen,TargetStaminaRegenDuration },
                {Stat.Mana,TargetManaDuration },
                {Stat.Weight,TargetWeightDuration },

            };


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
            Debug.Log($"Caster:{CasterObject.name}");
            Debug.Log($"Target:{TargetObject.name}");
            //Check if you can cast the spell
            float Weight = Caster[Stat.Weight];
            float Stamina = Caster[Stat.Stamina];
            float Mana = Caster[Stat.Mana];
            var OutputStaminaCost = StaminaCost * Weight / 80;
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
                float TargetHealthChange = -TargetOutputDamage * (1 - Target[Stat.DamageReduction] / 100);
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
            float SelfHealthChange = -SelfOutputDamage * (1 - Caster[Stat.DamageReduction] / 100);
            Caster[Stat.HealthPoints] += SelfHealthChange + SelfEffects[Stat.HealthPoints] ;
            float SelfMaxHealth = CasterObject.GetComponent<Character>().HealthPoints.Value;
            float SelfCurrentHealth = Caster[Stat.HealthPoints];
            if (SelfCurrentHealth / (SelfMaxHealth / 100) <= SelfFinisher)
            {
                Caster[Stat.HealthPoints] = 0;
            }
            foreach (var effect in SelfEffects)
            {
                if(effect.Value != 0 & SelfDelays[effect.Key] != 0)
                {
                    ActiveEffect activeEffect = new ActiveEffect();
                    activeEffect.delay = SelfDelays[effect.Key];
                    activeEffect.change = effect.Value;
                    activeEffect.duration = SelfDurations[effect.Key];
                    activeEffect.stat = effect.Key;
                    CasterObject.GetComponent<RealtimeStatsHolder>().delayedEffects.Add(activeEffect);
                }
                else if (effect.Value != 0 & SelfDelays[effect.Key] == 0)
                {
                    ActiveEffect activeEffect = new ActiveEffect();
                    activeEffect.delay = SelfDelays[effect.Key];
                    activeEffect.change = effect.Value;
                    activeEffect.duration = SelfDurations[effect.Key];
                    activeEffect.stat = effect.Key;
                    CasterObject.GetComponent<RealtimeStatsHolder>().activeEffects.Add(activeEffect);
                }
            }
            
            foreach (var effect in TargetEffects)
            {
                if (effect.Value != 0 & TargetDelays[effect.Key] != 0)
                {
                    ActiveEffect activeEffect = new ActiveEffect();
                    activeEffect.delay = TargetDelays[effect.Key];
                    activeEffect.change = effect.Value;
                    activeEffect.duration = TargetDurations[effect.Key]; ;
                    activeEffect.stat = effect.Key;
                    CasterObject.GetComponent<RealtimeStatsHolder>().delayedEffects.Add(activeEffect);
                }
                else if (effect.Value != 0 & TargetDelays[effect.Key] == 0)
                {
                    ActiveEffect activeEffect = new ActiveEffect();
                    activeEffect.delay = TargetDelays[effect.Key];
                    activeEffect.change = effect.Value;
                    activeEffect.duration = TargetDurations[effect.Key]; ;
                    activeEffect.stat = effect.Key;
                    CasterObject.GetComponent<RealtimeStatsHolder>().activeEffects.Add(activeEffect);
                }
            }

            
            








        }
#if UNITY_EDITOR
        [CustomEditor(typeof(Ability))]
        public class MyEditor : Editor
        {
            override public void OnInspectorGUI()
            {
                void AddOptions(string Name,ref float OriginalStat,ref float Delay,ref float Duration)
                {
                    EditorGUILayout.Space(5);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(Name, GUILayout.MaxWidth(150));
                    OriginalStat = EditorGUILayout.FloatField(OriginalStat);
                    EditorGUILayout.EndHorizontal();

                    if (OriginalStat != 0)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.Space(30);
                        EditorGUILayout.LabelField("Delay", GUILayout.MaxWidth(50));
                        Delay = EditorGUILayout.FloatField(Delay);
                        EditorGUILayout.LabelField("Duration", GUILayout.MaxWidth(60));
                        Duration = EditorGUILayout.FloatField(Duration);

                        EditorGUILayout.EndHorizontal();

                    }
                    

                }
               
                base.OnInspectorGUI();
                Ability myScript = (Ability)target;
                if (myScript.AffectsTarget)
                {
                    AddOptions("TargetHealthPoints", ref myScript.TargetHealthPoints, ref myScript.TargetHealthDelay, ref myScript.TargetHealthDuration);
                    AddOptions("TargetCritChance", ref myScript.TargetCritChance, ref myScript.TargetCritDelay, ref myScript.TargetCritDuration);
                    AddOptions("TargetDamage", ref myScript.TargetDamage, ref myScript.TargetDamageDelay, ref myScript.TargetDamageDuration);
                    AddOptions("TargetDamageReduction", ref myScript.TargetDamageReduction, ref myScript.TargetDamageReductionDelay, ref myScript.TargetDamageReductionDuration);
                    AddOptions("TargetDodge", ref myScript.TargetDodge, ref myScript.TargetDodgeDelay, ref myScript.TargetDodgeDuration);
                    AddOptions("TargetStamina", ref myScript.TargetStamina, ref myScript.TargetStaminaDelay, ref myScript.TargetStaminaDuration);
                    AddOptions("TargetStaminaRegen", ref myScript.TargetStaminaRegen, ref myScript.TargetStaminaRegenDelay, ref myScript.TargetStaminaRegenDuration);
                    AddOptions("TargetMana", ref myScript.TargetMana, ref myScript.TargetManaDelay, ref myScript.TargetManaDuration);
                    AddOptions("TargetWeight", ref myScript.TargetWeight, ref myScript.TargetWeightDelay, ref myScript.TargetWeightDuration);
                    EditorGUILayout.Space(5);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("TargetFinisher", GUILayout.MaxWidth(150));
                    myScript.TargetFinisher = EditorGUILayout.FloatField(myScript.TargetFinisher);
                    EditorGUILayout.EndHorizontal();

                }



                if (myScript.AffectsCaster)
                {
                    AddOptions("SelfHealthPoints", ref myScript.SelfHealthPoints, ref myScript.SelfHealthDelay, ref myScript.SelfHealthDuration);
                    AddOptions("SelfCritChance", ref myScript.SelfCritChance, ref myScript.SelfCritDelay, ref myScript.SelfCritDuration);
                    AddOptions("SelfDamage", ref myScript.SelfDamage, ref myScript.SelfDamageDelay, ref myScript.SelfDamageDuration);
                    AddOptions("SelfDamageReduction", ref myScript.SelfDamageReduction, ref myScript.SelfDamageReductionDelay, ref myScript.SelfDamageReductionDuration);
                    AddOptions("SelfDodge", ref myScript.SelfDodge, ref myScript.SelfDodgeDelay, ref myScript.SelfDodgeDuration);
                    AddOptions("SelfStamina", ref myScript.SelfStamina, ref myScript.SelfStaminaDelay, ref myScript.SelfStaminaDuration);
                    AddOptions("SelfStaminaRegen", ref myScript.SelfStaminaRegen, ref myScript.SelfStaminaRegenDelay, ref myScript.SelfStaminaRegenDuration);
                    AddOptions("SelfMana", ref myScript.SelfMana, ref myScript.SelfManaDelay, ref myScript.SelfManaDuration);
                    AddOptions("SelfWeight", ref myScript.SelfWeight, ref myScript.SelfWeightDelay, ref myScript.SelfWeightDuration);
                    EditorGUILayout.Space(5);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("SelfFinisher", GUILayout.MaxWidth(150));
                    myScript.SelfFinisher = EditorGUILayout.FloatField(myScript.SelfFinisher);
                    EditorGUILayout.EndHorizontal();

                }
                



            }
#endif


        }
    }

    


}

