using AE.FightManager;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static AbilityStorage;

public class EnemyBrain : MonoBehaviour
{
    public RealtimeStatsHolder PlayerHolder;
    public RealtimeStatsHolder EnemyHolder;
    public Character Player;
    public Character Enemy;
    public System.Linq.IOrderedEnumerable<KeyValuePair<List<AbilityName>,List<float>>> sortedStaminaDict;
    public System.Linq.IOrderedEnumerable<KeyValuePair<List<AbilityName>, List<float>>> sortedManaDict;
    public void Init()
    {

        List<AbilityName> List = null;
        List<List<AbilityName>> SecondList = new List<List<AbilityName>>();
        for (int a = 0; a < List.Count; a++)
        {
            SecondList.Add(new List<AbilityName>() { List[a] });

            for (int b = a + 1; b < List.Count; b++)
            {
                SecondList.Add(new List<AbilityName>() { List[a], List[b] });
                for (int c = b + 1; c < List.Count; c++)
                {
                    SecondList.Add(new List<AbilityName>() { List[a], List[b], List[c] });
                    for (int d = c + 1; d < List.Count; d++)
                    {
                        SecondList.Add(new List<AbilityName>() { List[a], List[b], List[c], List[d] });
                        for (int e = d + 1; e < List.Count; e++)
                        {
                            SecondList.Add(new List<AbilityName>() { List[a], List[b], List[c], List[d], List[e] });
                            for (int f = e + 1; f < List.Count; f++)
                            {
                                SecondList.Add(new List<AbilityName>() { List[a], List[b], List[c], List[d], List[e], List[f] });
                            }
                        }
                    }
                }

            }
        }
        Dictionary<List<AbilityName>, List<float>> AbilityCosts =new Dictionary<List<AbilityName>, List<float>>();
        foreach (var SpellCombination in SecondList)
        {
            float CombinedStaminaCost = 0;
            float CombinedManaCost = 0;
            foreach (var spell in SpellCombination)
            {
                CombinedStaminaCost += AbilityStorage.GetAbility[spell].StaminaCost ;
                CombinedManaCost += AbilityStorage.GetAbility[spell].ManaCost;
                foreach (var item in AbilityStorage.GetAbility[spell].CasterEffects)
                {
                    if(item.stat == Stat.Stamina & item.Duration == 0)
                    {
                        CombinedStaminaCost -= item.Change * (Enemy.Stamina.Value / 100);
                    }
                    else if (item.stat == Stat.Mana & item.Duration == 0)
                    {
                        CombinedManaCost -= item.Change * (Enemy.Mana.Value / 100);
                    }
                }
            }
            AbilityCosts[SpellCombination][0] = CombinedStaminaCost;
            AbilityCosts[SpellCombination][1] = CombinedManaCost;


        }
        sortedStaminaDict = from entry in AbilityCosts orderby entry.Value[0] ascending select entry;
      

    }
    public void MakeMove()
    {
        List<List<AbilityName>> CanCast = new List<List<AbilityName>>();
        foreach (var item in sortedStaminaDict)
        {
            if (item.Value[0] <= EnemyHolder.StatHolder[Stat.Stamina])
            {
                if (item.Value[1] <= EnemyHolder.StatHolder[Stat.Mana])
                {
                    CanCast.Add(item.Key);
                }
            }
            else
            {
                break;
            }

        }
        //Estimating Player Damage
        float EstimatedPlayerDamage = PlayerHolder.StatHolder[Stat.Damage];
        float EstimatedPlayerCritChance = PlayerHolder.StatHolder[Stat.CritChance];
        foreach (var ActiveEffect in PlayerHolder.activeEffects)
        {
            if (ActiveEffect.Key == Stat.Damage)
            {
                foreach (var effect in ActiveEffect.Value)
                {
                    if (effect.duration == 1)
                    {
                        EstimatedPlayerDamage -= effect.change * (Player.Damage.Value / 100);
                    }
                }

            }
            if (ActiveEffect.Key == Stat.CritChance)
            {
                foreach (var effect in ActiveEffect.Value)
                {
                    if (effect.duration == 1)
                    {
                        EstimatedPlayerCritChance -= effect.change;
                    }
                }

            }

        }
        
        foreach (var DelayedEffect in PlayerHolder.delayedEffects)
        {
            if (DelayedEffect.delay == 1 & DelayedEffect.stat == Stat.Damage)
            {
                EstimatedPlayerDamage += DelayedEffect.change * (Player.Damage.Value / 100);

            }
            if (DelayedEffect.delay == 1 & DelayedEffect.stat == Stat.CritChance)
            {
                EstimatedPlayerCritChance += DelayedEffect.change;

            }

        }
        //Estimating Enemy Damage
        float EstimatedEnemyDamage = EnemyHolder.StatHolder[Stat.Damage];
        float EstimatedEnemyCritChance = EnemyHolder.StatHolder[Stat.CritChance];
        foreach (var ActiveEffect in EnemyHolder.activeEffects)
        {
            if (ActiveEffect.Key == Stat.Damage)
            {
                foreach (var effect in ActiveEffect.Value)
                {
                    
                    EstimatedEnemyDamage -= effect.change * (Player.Damage.Value / 100);
                  
                }

            }
            if (ActiveEffect.Key == Stat.CritChance)
            {
                foreach (var effect in ActiveEffect.Value)
                {
                   
                EstimatedEnemyCritChance -= effect.change;
                    
                }

            }

        }

        foreach (var DelayedEffect in PlayerHolder.delayedEffects)
        {
            if (DelayedEffect.delay == 1 & DelayedEffect.stat == Stat.Damage)
            {
                EstimatedPlayerDamage += DelayedEffect.change * (Player.Damage.Value / 100);

            }
            if (DelayedEffect.delay == 1 & DelayedEffect.stat == Stat.CritChance)
            {
                EstimatedPlayerCritChance += DelayedEffect.change;

            }

        }
        //Estimating EnemyReductions
        float EstimatedEnemyDefence = Enemy.DamageReduction.Value;
        float EnemyShield = 0;
        float EstimatedEnemyDodgeChance = Enemy.DodgeChance.Value;
        float EstimatedEnemyHealthChange = 0;
        foreach (var ActiveEffect in EnemyHolder.activeEffects)
        {
            if (ActiveEffect.Key == Stat.DamageReduction)
            {
                foreach (var effect in ActiveEffect.Value)
                {
                    EstimatedEnemyDefence += effect.change;
                }
            }
            if (ActiveEffect.Key == Stat.HealthPoints)
            {
                foreach (var effect in ActiveEffect.Value)
                {
                    EnemyShield += effect.change;
                }
            }
            if (ActiveEffect.Key == Stat.DodgeChance)
            {
                foreach (var effect in ActiveEffect.Value)
                {
                    EstimatedEnemyDodgeChance += effect.change;
                }
            }

        }
        //Estimating Player Reductions
        
        float EstimatedPlayerDefence = Player.DamageReduction.Value;
        float PlayerShield = 0;
        float EstimatedPlayerDodgeChance = Player.DodgeChance.Value;
        float EstimatedPlayerHealthChange = 0;
        foreach (var ActiveEffect in PlayerHolder.activeEffects)
        {
            if (ActiveEffect.Key == Stat.DamageReduction)
            {
                foreach (var effect in ActiveEffect.Value)
                {
                    EstimatedPlayerDefence += effect.change;
                }
            }
            if (ActiveEffect.Key == Stat.HealthPoints)
            {
                foreach (var effect in ActiveEffect.Value)
                {
                    PlayerShield += effect.change;
                }
            }
            if (ActiveEffect.Key == Stat.DodgeChance)
            {
                foreach (var effect in ActiveEffect.Value)
                {
                    EstimatedPlayerDodgeChance += effect.change;
                }
            }

        }
        Dictionary<List<AbilityName>, float> BestOption = new Dictionary<List<AbilityName>, float>();
        foreach (var combo in CanCast)
        {
            float ComboEstimatedPlayerDamage = EstimatedPlayerDamage;
            float ComboEstimatedPlayerCritChance = EstimatedPlayerCritChance;
            float ComboEstimatedEnemyDefence = EstimatedEnemyDefence;
            float ComboEnemyShield = EnemyShield;
            float ComboEstimatedEnemyDodgeChance = EstimatedEnemyDodgeChance;
            float ComboEstimatedEnemyHealthChange = EstimatedEnemyHealthChange;
            

            foreach (var item in combo)
            {
                //Estimating Player Damage
                foreach (var effect in AbilityStorage.GetAbility[item].TargetEffects)
                {
                    if(effect.stat == Stat.Damage & effect.Delay == 0)
                    {
                        ComboEstimatedPlayerDamage += effect.Change * (Player.Damage.Value / 100);
                    }
                    if (effect.stat == Stat.CritChance & effect.Delay == 0)
                    {
                        ComboEstimatedPlayerCritChance += effect.Change ;
                    }
                }
                //Estimating Enemy DamageReductions
                foreach (var effect in AbilityStorage.GetAbility[item].CasterEffects)
                {
                    if (effect.stat == Stat.DodgeChance & effect.Delay == 0)
                    {
                        ComboEstimatedEnemyDodgeChance += effect.Change;
                    }
                    if (effect.stat == Stat.DamageReduction & effect.Delay == 0)
                    {
                        ComboEstimatedEnemyDefence += effect.Change;
                    }
                    if (effect.stat == Stat.HealthPoints & effect.Delay == 0)
                    {
                        if (effect.Duration == 0)
                        {
                            ComboEstimatedEnemyHealthChange += effect.Change * (Enemy.HealthPoints.Value / 100);
                        }
                        else
                        {
                            ComboEnemyShield += effect.Change * (Enemy.HealthPoints.Value / 100);
                        }
                    }
                }

            }
            //Estimating Damage Taken
            float ComboEstimatedEnemyDamageTaken = (ComboEstimatedPlayerDamage * (1 + ComboEstimatedPlayerCritChance)) * (1 - ComboEstimatedEnemyDefence) * (1 - ComboEstimatedEnemyDodgeChance) - ComboEstimatedEnemyHealthChange;
            if(ComboEnemyShield <= ComboEstimatedEnemyDamageTaken) 
            {
                ComboEstimatedEnemyDamageTaken -= ComboEnemyShield; 
            }
            else
            {
                ComboEstimatedEnemyDamageTaken = ComboEnemyShield - ComboEstimatedEnemyDamageTaken;
            }
            float ComboEnemyHealthPercentageChange = 100 / ( Enemy.HealthPoints.Value / ComboEstimatedEnemyDamageTaken );
            //Enemy Damage

            float ComboEstimatedEnemyDamage = EstimatedEnemyDamage;
            float ComboEstimatedEnemyCritChance = EstimatedEnemyCritChance;
            float ComboEstimatedPlayerDefence = EstimatedPlayerDefence;
            float ComboPlayerShield = PlayerShield;
            float ComboEstimatedPlayerDodgeChance = EstimatedPlayerDodgeChance;
            float ComboEstimatedPlayerHealthChange = EstimatedPlayerHealthChange;


            foreach (var item in combo)
            {
                //Estimating Player Damage
                foreach (var effect in AbilityStorage.GetAbility[item].CasterEffects)
                {
                    if (effect.stat == Stat.Damage & effect.Delay == 0)
                    {
                        ComboEstimatedEnemyDamage += effect.Change * (Enemy.Damage.Value / 100);
                    }
                    if (effect.stat == Stat.CritChance & effect.Delay == 0)
                    {
                        ComboEstimatedEnemyCritChance += effect.Change;
                    }
                }
                //Estimating Player DamageReductions
                foreach (var effect in AbilityStorage.GetAbility[item].TargetEffects)
                {
                    if (effect.stat == Stat.DodgeChance & effect.Delay == 0)
                    {
                        ComboEstimatedEnemyDodgeChance += effect.Change;
                    }
                    if (effect.stat == Stat.DamageReduction & effect.Delay == 0)
                    {
                        ComboEstimatedEnemyDefence += effect.Change;
                    }
                    if (effect.stat == Stat.HealthPoints & effect.Delay == 0)
                    {
                        if (effect.Duration == 0)
                        {
                            ComboEstimatedEnemyHealthChange += effect.Change * (Player.HealthPoints.Value / 100);
                        }
                        else
                        {
                            ComboEnemyShield += effect.Change * (Player.HealthPoints.Value / 100);
                        }
                    }
                }

            }
            //Estimating Damage Taken
            float ComboEstimatedPlayerDamageTaken = (ComboEstimatedEnemyDamage * (1 + ComboEstimatedEnemyCritChance)) * (1 - ComboEstimatedPlayerDefence) * (1 - ComboEstimatedPlayerDodgeChance) - ComboEstimatedPlayerHealthChange;
            if (ComboPlayerShield <= ComboEstimatedPlayerDamageTaken)
            {
                ComboEstimatedPlayerDamageTaken -= ComboPlayerShield;
            }
            else
            {
                ComboEstimatedPlayerDamageTaken = ComboPlayerShield - ComboEstimatedPlayerDamageTaken;
            }
            float ComboPlayerHealthPercentageChange = 100 / (Player.HealthPoints.Value / ComboEstimatedPlayerDamageTaken);

            BestOption[combo] = ComboPlayerHealthPercentageChange + ComboEnemyHealthPercentageChange;




        }
        var SortedBestOption = from entry in BestOption orderby entry.Value descending select entry;

        print(SortedBestOption.First());
        var ChosenCombo = SortedBestOption.First();



    }
}
