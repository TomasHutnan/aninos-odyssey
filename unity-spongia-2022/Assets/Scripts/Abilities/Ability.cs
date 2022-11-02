using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AE.FightManager;
using Unity.VisualScripting.FullSerializer;
using System.Diagnostics;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Abilities
{
    [System.Serializable]
    public class CreationAbility
    {
        public Stat stat;
        public float Change;
        public float Duration;
        public float Delay;
    }
    class StatProperties
    {
   
        public float Maximum;
        
        public StatType StatType;
        public StatProperties(float _Maximum, StatType _StatType)
        {
            
            Maximum = _Maximum;
           
            StatType = _StatType;
        }
    }
    public enum AbilityTags
    {
        //Effected stats
        Healt,
        Mana,
        Stamina,
        Damage,
        CritChance,
        Defense,
        DodgeChance,
        Weight,

        //Attack abilities
        PriestAttack,
        FighterAttack,
        TankAttack,
        RogueAttack,
        StunAttack,

        //Miscellaneous
        CasterBuff,
        CasterDebuff,
        TargetBuff,
        TargerDebuff,
        Delay,
        Temporary,
        Permanent,
    }
    [CreateAssetMenu]
    public class Ability : ScriptableObject
    {
        public List<AbilityTags> AbilityTags;
        public string AbilityDescription;
        public int AbilityCount = 1;
        public int StaminaCost;
        public int ManaCost;
        public float TargetDamageMultiplier;
        public float CasterDamageMultiplier;
        public bool AffectsTarget;
        public bool AffectsCaster;

        public float TargetFinisher;
        public float CasterFinisher;





        [SerializeField]
        public List<CreationAbility> TargetEffects;
        [SerializeField]
        public List<CreationAbility> CasterEffects;




        public void UseAbility(GameObject CasterObject, GameObject TargetObject)
        {
        
           
            RealtimeStatsHolder TargetHolder = TargetObject.GetComponent<RealtimeStatsHolder>();
            RealtimeStatsHolder CasterHolder = CasterObject.GetComponent<RealtimeStatsHolder>();
            Character TargetCharacter = TargetHolder._fighter;
            Character CasterCharacter = CasterHolder._fighter;
            if (CasterHolder.StatHolder[Stat.Stun] != 0) { return; }
            Dictionary<Stat, StatProperties> CasterStats = new Dictionary<Stat, StatProperties>()
            {
                {Stat.CritChance,new StatProperties(100,StatType.Percentual) },
                {Stat.Damage,new StatProperties(CasterCharacter.Damage.Value,StatType.Flat) },
                {Stat.DamageReduction,new StatProperties(100,StatType.Percentual) },
                {Stat.DodgeChance,new StatProperties(100,StatType.Percentual) },
                {Stat.Stamina,new StatProperties(CasterCharacter.Stamina.Value,StatType.Flat) },
                {Stat.StaminaRegen,new StatProperties(1,StatType.Percentual) },
                {Stat.Mana,new StatProperties(CasterCharacter.Mana.Value,StatType.Flat) },
                {Stat.Weight,new StatProperties(CasterCharacter.Weight.Value,StatType.Flat) },
                {Stat.Stun,new StatProperties(1,StatType.Stun) },
                {Stat.HealthPoints,new StatProperties(CasterCharacter.HealthPoints.Value,StatType.Flat) },
            };
            Dictionary<Stat, StatProperties> TargetStats = new Dictionary<Stat, StatProperties>()
            {
                {Stat.CritChance,new StatProperties(100,StatType.Percentual) },
                {Stat.Damage,new StatProperties(TargetCharacter.Damage.Value,StatType.Flat) },
                {Stat.DamageReduction,new StatProperties(100,StatType.Percentual) },
                {Stat.DodgeChance,new StatProperties(100,StatType.Percentual) },
                {Stat.Stamina,new StatProperties(TargetCharacter.Stamina.Value,StatType.Flat) },
                {Stat.StaminaRegen,new StatProperties(1,StatType.Percentual) },
                {Stat.Mana,new StatProperties(TargetCharacter.Mana.Value,StatType.Flat) },
                {Stat.Weight,new StatProperties(TargetCharacter.Weight.Value,StatType.Flat) },
                {Stat.Stun,new StatProperties(1,StatType.Stun) },
                {Stat.HealthPoints,new StatProperties(TargetCharacter.HealthPoints.Value,StatType.Flat) },
            };




            

            var Caster = CasterHolder.StatHolder;
            var Target = TargetHolder.StatHolder;
            //Check if you can cast the spell
            float Weight = Caster[Stat.Weight];
            float Stamina = Caster[Stat.Stamina];
            float Mana = Caster[Stat.Mana];
            var OutputStaminaCost = StaminaCost * Weight / 80;
            if (Stamina < OutputStaminaCost || Mana < ManaCost) { return; }
            //Stamina and Mana taking
            Caster[Stat.Mana] -= ManaCost;
            Caster[Stat.Stamina] -= OutputStaminaCost;
            

            foreach (var item in TargetEffects)
            {

                if (item.stat == Stat.Stun)
                {
                    float change = item.Change;
                    ActiveEffect effect = new ActiveEffect(change, item.stat, item.Duration, item.Delay, TargetStats[item.stat].StatType);
                    TargetHolder.delayedEffects.Add(effect);

                }
                else if(item.stat == Stat.HealthPoints)
                {
                    float Damage = Caster[Stat.Damage];
                    float CriticalChance = Caster[Stat.CritChance];
                    int Chance = UnityEngine.Random.Range(1, 101);
                    int DodgeRoll = UnityEngine.Random.Range(1, 101);
                    float DodgeChance = Target[Stat.DodgeChance];
                    int DodgeMultiplier = DodgeRoll <= DodgeChance ? 0 : 1;
                    int CasterCriticalMultiplier = Chance <= CriticalChance ? 2 : 1;
                    //Calculating CasterOutput Damage
                    float CasterOutputDamage = (float)Math.Round(Damage * CasterDamageMultiplier) * CasterCriticalMultiplier * DodgeMultiplier;

                    float change =( item.Change * (TargetStats[item.stat].Maximum / 100) ) + (-CasterOutputDamage * (1 - Target[Stat.DamageReduction] / 100));
                    ActiveEffect effect = new ActiveEffect(change, item.stat, item.Duration, item.Delay, TargetStats[item.stat].StatType);
                    TargetHolder.delayedEffects.Add(effect);

                }
                else if(item.Change != 0)
                {
                    
                    float change = item.Change * (TargetStats[item.stat].Maximum/100);
                    ActiveEffect effect = new ActiveEffect(change, item.stat, item.Duration, item.Delay, TargetStats[item.stat].StatType);
                    TargetHolder.delayedEffects.Add(effect);
                }
            }
            foreach (var item in CasterEffects)
            {
                if(item.stat == Stat.Stun)
                {
                    float change = item.Change;
                    ActiveEffect effect = new ActiveEffect(change, item.stat, item.Duration, item.Delay, CasterStats[item.stat].StatType);
                    CasterHolder.delayedEffects.Add(effect);

                }
                else if(item.stat == Stat.HealthPoints)
                {
                    float Damage = Caster[Stat.Damage];
                    float CriticalChance = Caster[Stat.CritChance];
                    int DodgeRoll = UnityEngine.Random.Range(1, 101);
                    float DodgeChance = Caster[Stat.DodgeChance];
                    int DodgeMultiplier = DodgeRoll <= DodgeChance ? 0 : 1;
                    int Chance = UnityEngine.Random.Range(1, 101);
                    int TargetCriticalMultiplier = Chance <= CriticalChance ? 2 : 1;
                    //Caclulating TargetOutput Damage
                    float TargetOutputDamage = (float)Math.Round(Damage * CasterDamageMultiplier) * TargetCriticalMultiplier * DodgeMultiplier;
                   
                    float change = (item.Change * (CasterStats[item.stat].Maximum / 100)) + (-TargetOutputDamage * (1 - Caster[Stat.DamageReduction] / 100));
                    ActiveEffect effect = new ActiveEffect(change, item.stat, item.Duration, item.Delay, CasterStats[item.stat].StatType);
                    CasterHolder.delayedEffects.Add(effect);

                }
                else if (item.Change != 0)
                {
                    
                    float change = item.Change * (CasterStats[item.stat].Maximum / 100);
                    ActiveEffect effect = new ActiveEffect(change, item.stat,item.Delay ,item.Duration, CasterStats[item.stat].StatType);
                    CasterHolder.delayedEffects.Add(effect);
                }
            }

            //All spells that dont affect Caster
            if (Target != null)
            { 
                float TargetMaxHealth = TargetCharacter.HealthPoints.Value;
                float TargetCurrentHealth = Target[Stat.HealthPoints];

                if (TargetCurrentHealth / (TargetMaxHealth / 100) <= TargetFinisher & TargetFinisher != 0)
                {
                    Target[Stat.HealthPoints] = 0;
                }
            }
            float CasterMaxHealth = CasterCharacter.HealthPoints.Value;
            float CasterCurrentHealth = Caster[Stat.HealthPoints];
            if (CasterCurrentHealth / (CasterMaxHealth / 100) <= CasterFinisher & CasterFinisher != 0)
            {
                Caster[Stat.HealthPoints] = 0;
            }
            
            
            

            
            








        }




        }
    

    


}

