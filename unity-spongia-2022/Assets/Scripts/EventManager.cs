using AE.CharacterStats;
using AE.Items;
using AE.Items.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using static AbilityStorage;

namespace AE.EventManager
{
    public class EventManager
    {
        public static event Action<Item> OnItemSlotEnterEvent;
        public static event Action OnItemSlotExitEvent;

        public static event Action<AbilityName> OnAbilitySlotEnterEvent;
        public static event Action OnAbilitySlotExitEvent;

        public static event Action<ActiveEffect> OnActiveEffectSlotEnterEvent;
        public static event Action OnActiveEffectSlotExitEvent;

        public static event Action<CharacterStat> OnStatEnterEvent;
        public static event Action OnStatExitEvent;

        public static event Action OnSoundSettingsUpdate;

        public static event Action<Item, PromptType> ItemPromptQuestionEvent;
        public static event Action<Item, PromptType, bool> ItemPromptAnswerEvent;

        public static void TriggerItemSlotEnter(Item item)
        {
            OnItemSlotEnterEvent?.Invoke(item);
        }
        public static void TriggerItemSlotExit()
        {
            OnItemSlotExitEvent?.Invoke();
        }

        public static void TriggerAbilitySlotEnter(AbilityName abilityName)
        {
            OnAbilitySlotEnterEvent?.Invoke(abilityName);
        }
        public static void TriggerAbilitySlotExit()
        {
            OnAbilitySlotExitEvent?.Invoke();
        }

        public static void TriggerActiveEffectSlotEnter(ActiveEffect activeEffect)
        {
            OnActiveEffectSlotEnterEvent?.Invoke(activeEffect);
        }
        public static void TriggerActiveEffectSlotExit()
        {
            OnActiveEffectSlotExitEvent?.Invoke();
        }

        public static void TriggerStatEnter(CharacterStat stat)
        {
            OnStatEnterEvent?.Invoke(stat);
        }
        public static void TriggerStatExit()
        {
            OnStatExitEvent?.Invoke();
        }

        public static void TriggerSoundSettingsUpdate()
        {
            OnSoundSettingsUpdate?.Invoke();
        }

        public static void TriggerItemPromptQuestion(Item item, PromptType promptType)
        {
            ItemPromptQuestionEvent?.Invoke(item, promptType);
        }
        public static void TriggerItemPromptAnswer(Item item, PromptType promptType, bool answer)
        {
            ItemPromptAnswerEvent?.Invoke(item, promptType, answer);
        }
    }
}
