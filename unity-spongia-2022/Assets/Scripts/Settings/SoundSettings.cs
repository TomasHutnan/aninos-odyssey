using AE.GameSave;
using AE.EventManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AE.Settings.UI
{
    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] Toggle musicToggle;
        [SerializeField] Toggle sfxToggle;
        void OnEnable()
        {
            musicToggle.isOn = !SaveData.IsMuted[0];
            sfxToggle.isOn = !SaveData.IsMuted[1];
        }

        public void ToggleMusic(bool value)
        {
            SaveData.IsMuted[0] = !value;
            EventManager.EventManager.TriggerSoundSettingsUpdate();
        }
        public void ToggleSFX(bool value)
        {
            SaveData.IsMuted[1] = !value;
            EventManager.EventManager.TriggerSoundSettingsUpdate();
        }
    }
}
