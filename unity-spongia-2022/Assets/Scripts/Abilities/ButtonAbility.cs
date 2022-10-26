using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
public class ButtonAbility : MonoBehaviour
{
    public void Daco(Ability so,GameObject Caster,GameObject Target)
    {
        so.UseAbility(Caster, Target);
    }
}
