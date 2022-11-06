using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEffectSlot : MonoBehaviour
{

    private ActiveEffect _activeEffect;
    public ActiveEffect ActiveEffect
    {
        set
        {
            _activeEffect = value;
        }
        get
        {
            return _activeEffect;
        }
    }

}
