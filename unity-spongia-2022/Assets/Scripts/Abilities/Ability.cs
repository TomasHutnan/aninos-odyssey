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
    class StatProperties
    {
        public float Change;
        public float Maximum;
        public float Duration;
        public float Delay;
        public StatType StatType;
        public StatProperties(float _Change,float _Maximum, float _Duration,float _Delay, StatType _StatType)
        {
            Change = _Change;
            Maximum = _Maximum;
            Duration = _Duration;
            Delay = _Delay;
            StatType = _StatType;
        }
    }
    [CreateAssetMenu]
    public class Ability : ScriptableObject
    {
        public int StaminaCost;
        public int ManaCost;
        public float TargetDamageMultiplier;
        public float CasterDamageMultiplier;
        public bool AffectsTarget;
        public bool AffectsCaster;

        //TargetEffects

       float TargetHealthPoints;
        float TargetHealthDuration;
        float TargetHealthDelay;

        float TargetCritChance;
        float TargetCritDuration;
        float TargetCritDelay;

        float TargetDamage;
        float TargetDamageDuration;
        float TargetDamageDelay;


        float TargetDamageReduction;
        float TargetDamageReductionDuration;
        float TargetDamageReductionDelay;


         float TargetDodge;
        float TargetDodgeDuration;
        float TargetDodgeDelay;


        float TargetStamina;
        float TargetStaminaDuration;
        float TargetStaminaDelay;


       float TargetStaminaRegen;
        float TargetStaminaRegenDuration;
        float TargetStaminaRegenDelay;


        float TargetMana;
        float TargetManaDuration;
        float TargetManaDelay;


       float TargetWeight;
        float TargetWeightDuration;
        float TargetWeightDelay;


        float TargetFinisher;



        //CasterEffects

        float CasterHealthPoints;
        float CasterHealthDuration;
        float CasterHealthDelay;

        float CasterCritChance;
        float CasterCritDuration;
        float CasterCritDelay;

         float CasterDamage;
        float CasterDamageDuration;
        float CasterDamageDelay;


        float CasterDamageReduction;
        float CasterDamageReductionDuration;
        float CasterDamageReductionDelay;


        float CasterDodge;
        float CasterDodgeDuration;
        float CasterDodgeDelay;

        float CasterStamina;
        float CasterStaminaDuration;
        float CasterStaminaDelay;

        float CasterStaminaRegen;
        float CasterStaminaRegenDuration;
        float CasterStaminaRegenDelay;

        float CasterMana;
         float CasterManaDuration;
         float CasterManaDelay;



         float CasterWeight;
         float CasterWeightDuration;
         float CasterWeightDelay;


        float CasterFinisher;




     






        public void UseAbility(GameObject CasterObject, GameObject TargetObject)
        {
            Character TargetCharacter = TargetObject.GetComponent<Character>();
            Character CasterCharacter = CasterObject.GetComponent<Character>();
            Dictionary<Stat, StatProperties> CasterStats = new Dictionary<Stat, StatProperties>()
            {
                {Stat.HealthPoints,new StatProperties(CasterHealthPoints,CasterCharacter.HealthPoints.Value,CasterHealthDuration,CasterHealthDelay,StatType.Flat) },
                {Stat.CritChance,new StatProperties(CasterCritChance,100,CasterCritDuration,CasterCritDelay,StatType.Percentual) },
                {Stat.Damage,new StatProperties(CasterDamage,CasterCharacter.Damage.Value,CasterDamageDuration,CasterDamageDelay,StatType.Flat) },
                {Stat.DamageReduction,new StatProperties(CasterDamageReduction,100,CasterDamageReductionDuration,CasterDamageReductionDelay,StatType.Percentual) },
                {Stat.DodgeChance,new StatProperties(CasterDodge,100,CasterDodgeDuration,CasterDodgeDelay,StatType.Percentual) },
                {Stat.Stamina,new StatProperties(CasterStamina,CasterCharacter.Stamina.Value,CasterStaminaDuration,CasterStaminaDelay,StatType.Flat) },
                {Stat.StaminaRegen,new StatProperties(CasterStaminaRegen,CasterCharacter.StaminaRegen.Value,CasterStaminaRegenDuration,CasterStaminaRegenDelay,StatType.Flat) },
                {Stat.Mana,new StatProperties(CasterMana,CasterCharacter.Mana.Value,CasterManaDuration,CasterManaDelay,StatType.Flat) },
                {Stat.Weight,new StatProperties(CasterWeight,CasterCharacter.Weight.Value,CasterWeightDuration,CasterWeightDelay,StatType.Flat) },

            };
            Dictionary<Stat, StatProperties> TargetStats = new Dictionary<Stat, StatProperties>()
            {
                {Stat.HealthPoints,new StatProperties(TargetHealthPoints,TargetCharacter.HealthPoints.Value,TargetHealthDuration,TargetHealthDelay,StatType.Flat) },
                {Stat.CritChance,new StatProperties(TargetCritChance,100,TargetCritDuration,TargetCritDelay,StatType.Percentual) },
                {Stat.Damage,new StatProperties(TargetDamage,TargetCharacter.Damage.Value,TargetDamageDuration,TargetDamageDelay,StatType.Flat) },
                {Stat.DamageReduction,new StatProperties(TargetDamageReduction,100,TargetDamageReductionDuration,TargetDamageReductionDelay,StatType.Percentual) },
                {Stat.DodgeChance,new StatProperties(TargetDodge,100,TargetDodgeDuration,TargetDodgeDelay,StatType.Percentual) },
                {Stat.Stamina,new StatProperties(TargetStamina,TargetCharacter.Stamina.Value,TargetStaminaDuration,TargetStaminaDelay,StatType.Flat) },
                {Stat.StaminaRegen,new StatProperties(TargetStaminaRegen,TargetCharacter.StaminaRegen.Value,TargetStaminaRegenDuration,TargetStaminaRegenDelay,StatType.Flat) },
                {Stat.Mana,new StatProperties(TargetMana,TargetCharacter.Mana.Value,TargetManaDuration,TargetManaDelay,StatType.Flat) },
                {Stat.Weight,new StatProperties(TargetWeight,TargetCharacter.Weight.Value,TargetWeightDuration,TargetWeightDelay,StatType.Flat) },

            };




            

            var Caster = CasterObject.GetComponent<RealtimeStatsHolder>().StatHolder;
            var Target = TargetObject.GetComponent<RealtimeStatsHolder>().StatHolder;
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
            int TargetCriticalMultiplier = Chance <= CriticalChance ? 2 : 1;
            Chance = UnityEngine.Random.Range(1, 101);
            int CasterCriticalMultiplier = Chance <= CriticalChance ? 2 : 1;
            //Caclulating TargetOutput Damage
            float TargetOutputDamage = (float)Math.Round(Target[Stat.Damage] * TargetDamageMultiplier) * TargetCriticalMultiplier;
            //Calculating CasterOutput Damage
            float CasterOutputDamage = (float)Math.Round(Caster[Stat.Damage] * CasterDamageMultiplier) * CasterCriticalMultiplier;


            foreach (var item in TargetStats)
            {
                if (item.Key == Stat.HealthPoints)
                {
                    float change =( item.Value.Change * (item.Value.Maximum / 100) ) + (-CasterOutputDamage * (1 - Target[Stat.DamageReduction] / 100));
                    ActiveEffect effect = new ActiveEffect(change, item.Key, item.Value.Duration, item.Value.Delay, item.Value.StatType);
                    TargetObject.GetComponent<RealtimeStatsHolder>().delayedEffects.Add(effect);

                }
                else if(item.Value.Change != 0)
                {
                    float change = item.Value.Change * (item.Value.Maximum/100);
                    ActiveEffect effect = new ActiveEffect(change, item.Key, item.Value.Duration, item.Value.Delay, item.Value.StatType);
                    TargetObject.GetComponent<RealtimeStatsHolder>().delayedEffects.Add(effect);
                }
            }
            foreach (var item in CasterStats)
            {
                if(item.Key == Stat.HealthPoints)
                {
                    float change = (item.Value.Change * (item.Value.Maximum / 100)) + (-TargetOutputDamage * (1 - Caster[Stat.DamageReduction] / 100));
                    ActiveEffect effect = new ActiveEffect(change, item.Key, item.Value.Duration, item.Value.Delay, item.Value.StatType);
                    CasterObject.GetComponent<RealtimeStatsHolder>().delayedEffects.Add(effect);

                }
                else if (item.Value.Change != 0)
                {
                    float change = item.Value.Change * (item.Value.Maximum / 100);
                    ActiveEffect effect = new ActiveEffect(change, item.Key, item.Value.Duration, item.Value.Delay, item.Value.StatType);
                    CasterObject.GetComponent<RealtimeStatsHolder>().delayedEffects.Add(effect);
                }
            }

            //All spells that dont affect Caster
            if (Target != null)
            { 
                float TargetMaxHealth = TargetObject.GetComponent<Character>().HealthPoints.Value;
                float TargetCurrentHealth = Target[Stat.HealthPoints];

                if (TargetCurrentHealth / (TargetMaxHealth / 100) <= TargetFinisher & TargetFinisher != 0)
                {
                    Target[Stat.HealthPoints] = 0;
                }
            }
            float CasterMaxHealth = CasterObject.GetComponent<Character>().HealthPoints.Value;
            float CasterCurrentHealth = Caster[Stat.HealthPoints];
            if (CasterCurrentHealth / (CasterMaxHealth / 100) <= CasterFinisher & CasterFinisher != 0)
            {
                Caster[Stat.HealthPoints] = 0;
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
                    AddOptions("CasterHealthPoints", ref myScript.CasterHealthPoints, ref myScript.CasterHealthDelay, ref myScript.CasterHealthDuration);
                    AddOptions("CasterCritChance", ref myScript.CasterCritChance, ref myScript.CasterCritDelay, ref myScript.CasterCritDuration);
                    AddOptions("CasterDamage", ref myScript.CasterDamage, ref myScript.CasterDamageDelay, ref myScript.CasterDamageDuration);
                    AddOptions("CasterDamageReduction", ref myScript.CasterDamageReduction, ref myScript.CasterDamageReductionDelay, ref myScript.CasterDamageReductionDuration);
                    AddOptions("CasterDodge", ref myScript.CasterDodge, ref myScript.CasterDodgeDelay, ref myScript.CasterDodgeDuration);
                    AddOptions("CasterStamina", ref myScript.CasterStamina, ref myScript.CasterStaminaDelay, ref myScript.CasterStaminaDuration);
                    AddOptions("CasterStaminaRegen", ref myScript.CasterStaminaRegen, ref myScript.CasterStaminaRegenDelay, ref myScript.CasterStaminaRegenDuration);
                    AddOptions("CasterMana", ref myScript.CasterMana, ref myScript.CasterManaDelay, ref myScript.CasterManaDuration);
                    AddOptions("CasterWeight", ref myScript.CasterWeight, ref myScript.CasterWeightDelay, ref myScript.CasterWeightDuration);
                    EditorGUILayout.Space(5);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("CasterFinisher", GUILayout.MaxWidth(150));
                    myScript.CasterFinisher = EditorGUILayout.FloatField(myScript.CasterFinisher);
                    EditorGUILayout.EndHorizontal();

                }
                



            }
#endif


        }
    }

    


}

