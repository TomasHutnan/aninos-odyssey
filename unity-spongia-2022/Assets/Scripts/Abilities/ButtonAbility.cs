using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Abilities;
using AE.FightManager;
using static AbilityStorage;
using AE.Fight.UI;

public class ButtonAbility : MonoBehaviour
{
    public FightSFXProvider fightSFX;
    public int AbbilityNumber;
    [SerializeField] StanceController stanceController;

    public void Cast()
    {
        GameObject Target = null;
        Transform FightManager = gameObject.transform.parent.parent;
        for (int i = 0; i < FightManager.childCount; i++)
        {
            var child = FightManager.GetChild(i);
            if(child != transform.parent)
            {
               
                AbilityName abilityName = transform.parent.gameObject.GetComponent<RealtimeStatsHolder>().AvailableAbilities[AbbilityNumber];
                if (abilityName == AbilityName.None) { return; }
                Ability ability = AbilityStorage.GetAbility[abilityName];
              
                for (int x = 0; x < ability.AbilityCount; x++)
                {
                    print("Castujem");
                    Target = child.gameObject;
                    ability.UseAbility(transform.parent.gameObject, Target, stanceController);

                    if (ability.AbilityAnimationType == StanceType.Attack)
                    {
                        AE.Items.Item weapon;
                        bool contains = transform.parent.GetComponent<RealtimeStatsHolder>()._fighter.EquippedItems.TryGetValue(AE.Items.ItemType.Weapon, out weapon);
                        if (contains || weapon != null)
                            fightSFX.PlaySFX((AnimationWeaponClass)weapon.Class);
                        else
                            fightSFX.PlaySFX(AnimationWeaponClass.NoWeapon);
                    }

                }
                transform.parent.gameObject.GetComponent<RealtimeStatsHolder>().AvailableAbilities[AbbilityNumber] = AbilityName.None;

            }

        }
    }
}
