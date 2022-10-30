using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
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
                Ability ability = transform.parent.gameObject.GetComponent<Character>().Ability[AbbilityNumber];
                for (int x = 0; x < ability.AbilityCount; x++)
                {
                    Target = child.gameObject;
                    ability.UseAbility(transform.parent.gameObject, Target);

                }
                

            }

        }
    }
}
