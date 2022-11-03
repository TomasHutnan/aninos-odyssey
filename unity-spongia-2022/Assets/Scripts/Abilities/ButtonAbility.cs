using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
using AE.FightManager;
using static AbilityStorage;

public class ButtonAbility : MonoBehaviour
{
    public int AbbilityNumber;

    public void Cast()
    {
        GameObject Target = null;
        Transform FightManager = gameObject.transform.parent.parent;
        for (int i = 0; i < FightManager.childCount; i++)
        {
            var child = FightManager.GetChild(i);
            if(child != transform.parent)
            {
                print(transform.parent.gameObject.name);
                AbilityName abilityName = transform.parent.gameObject.GetComponent<RealtimeStatsHolder>().AvailableAbilities[AbbilityNumber];
                if (abilityName == AbilityName.None) { return; }
                Ability ability = AbilityStorage.GetAbility[abilityName];
              
                for (int x = 0; x < ability.AbilityCount; x++)
                {
                    Target = child.gameObject;
                    ability.UseAbility(transform.parent.gameObject, Target);

                }
                transform.parent.gameObject.GetComponent<RealtimeStatsHolder>().AvailableAbilities[AbbilityNumber] = AbilityName.None;

            }

        }
    }
}
