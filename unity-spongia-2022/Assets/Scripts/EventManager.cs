using AE.CharacterStats;
using AE.Items;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AE.EventManager
{
    public class EventManager
    {
        public static event Action<Item> OnItemSlotEnterEvent;
        public static event Action OnItemSlotExitEvent;

        public static event Action<CharacterStat> OnStatEnterEvent;
        public static event Action OnStatExitEvent;

        public static event Action OnSoundSettingsUpdate;

        public static void TriggerItemSlotEnter(Item item)
        {
            OnItemSlotEnterEvent?.Invoke(item);
        }
        public static void TriggerItemSlotExit()
        {
            OnItemSlotExitEvent?.Invoke();
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
    }
}
