using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AE.CharacterStats;
using AE.FightManager;
using AE.Abilities;
using Abilities;
using static AbilityStorage;

public enum StatType
{
    Flat,
    Percentual,
    Stun
            
}
public class ActiveEffect
{
    
    public float change;
    public Stat stat;
    public float duration;
    public float delay;
    public StatType type;

    public AbilityName abilityName = AbilityName.None;
    
    public ActiveEffect(float _change,Stat _stat,float _duration,float _delay,StatType _type )
    {
        change = _change;
        stat = _stat;   
        duration = _duration;   
        delay = _delay;
        type = _type;

    }
    

    
}