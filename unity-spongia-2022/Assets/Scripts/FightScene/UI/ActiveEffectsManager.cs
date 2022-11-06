using AE.Abilities.UI;
using AE.FightManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEffectsManager : MonoBehaviour
{
    [SerializeField] ActiveEffectSlot[] activeEffectSlots;
    private int currentEffectsCount = 0;

    private void Start()
    {
        currentEffectsCount = 0;
        for (; currentEffectsCount < activeEffectSlots.Length;)
            AddEfect(null);
        currentEffectsCount = 0;
    }

    public void UpdateAllEfects(Dictionary<Stat, List<ActiveEffect>> statDict)
    {
        currentEffectsCount = 0;
        foreach (List<ActiveEffect> statList in statDict.Values)
        {
            foreach (ActiveEffect effect in statList)
            {
                AddEfect(effect);
            }
        }
        int tempCurrentEffectCount = currentEffectsCount;
        for (; currentEffectsCount < activeEffectSlots.Length;)
            AddEfect(null);
        currentEffectsCount = tempCurrentEffectCount;
    }

    public void AddEfect(ActiveEffect effect)
    {
        if (effect != null)
            print($"ADDED EFFECT {effect.stat}");
        else
            print("EFFECT IS NULL");
        if (currentEffectsCount >= activeEffectSlots.Length)
            return;

        activeEffectSlots[currentEffectsCount].ActiveEffect = effect;
        currentEffectsCount++;
    }

    private void OnValidate()
    {
        activeEffectSlots = GetComponentsInChildren<ActiveEffectSlot>();
    }
}
