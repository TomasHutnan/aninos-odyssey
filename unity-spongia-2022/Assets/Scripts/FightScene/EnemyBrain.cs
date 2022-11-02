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
        foreach (var combo in CanCast)
        {
            //Estimating Player Damage
            float EstimatedPlayerDamage = PlayerHolder.StatHolder[Stat.Damage];
            foreach (var ActiveEffect in PlayerHolder.activeEffects)
            {
                if(ActiveEffect.Key == Stat.Damage)
                {
                    foreach (var effect in ActiveEffect.Value)
                    {
                        if(effect.duration == 1)
                        {
                            EstimatedPlayerDamage -= effect.change;
                        }
                    }

                }

            }
            foreach (var DelayedEffect in PlayerHolder.delayedEffects)
            {
               if(DelayedEffect.delay == 1 & DelayedEffect.stat == Stat.Damage)
                {
                    EstimatedPlayerDamage += DelayedEffect.change;

                }

            }
            foreach (var item in combo)
            {
                foreach (var item in AbilityStorage.GetAbility[item].TargetEffects)
                {

                }

            }

        }

        


    }
}
