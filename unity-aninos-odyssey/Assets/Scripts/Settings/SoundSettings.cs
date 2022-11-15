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
        void Start()
        {
            musicToggle.isOn = !Preferences.MuteMusic;
            sfxToggle.isOn = !Preferences.MuteSFX;
        }

        public void ToggleMusic(bool value)
        {
            Preferences.MuteMusic = !value;
            EventManager.EventManager.TriggerSoundSettingsUpdate();
        }
        public void ToggleSFX(bool value)
        {
            Preferences.MuteSFX = !value;
            EventManager.EventManager.TriggerSoundSettingsUpdate();
        }
    }
}
