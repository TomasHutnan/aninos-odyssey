using AE.Abilities;
using AE.EventManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static AbilityStorage;

public class ActiveEffectSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image background;
    [SerializeField] Image icon;
    [SerializeField] Image[] foregrounds;

    private ActiveEffect _activeEffect;
    public ActiveEffect ActiveEffect
    {
        set
        {
            _activeEffect = value;

            if (_activeEffect == null || _activeEffect.abilityName == AbilityName.None)
            {
                _activeEffect = null;
                background.enabled = false;
                icon.enabled = false;
                foregrounds[0].enabled = false;
                foregrounds[1].enabled = false;
            }
            else
            {
                background.sprite = AbilityImages.GetBG(); // ability.icon_bg
                background.enabled = true;

                icon.sprite = GetAbilityIcon(_activeEffect.abilityName); // ability.icon
                icon.enabled = true;

                foregrounds[0].sprite = AbilityImages.GetLevelFG(_activeEffect.abilityName); // ability.icon_fg
                foregrounds[0].enabled = true;
                if (foregrounds[0].sprite == null)
                    foregrounds[0].enabled = false;

                foregrounds[1].sprite = AbilityImages.GetBuffFG(_activeEffect.stat, _activeEffect.change); // ability.icon_fg
                foregrounds[1].enabled = true;
                if (foregrounds[1].sprite == null)
                    foregrounds[1].enabled = false;
            }
        }
        get
        {
            return _activeEffect;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_activeEffect != null)
            EventManager.TriggerActiveEffectSlotEnter(_activeEffect);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventManager.TriggerActiveEffectSlotExit();
    }

}
