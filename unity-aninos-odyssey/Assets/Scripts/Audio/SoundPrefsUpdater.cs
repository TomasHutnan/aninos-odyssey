using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AE.GameSave;
using AE.EventManager;

namespace AE.Audio
{
    public class SoundPrefsUpdater : MonoBehaviour
    {
        [SerializeField] AudioSource[] audioSources;

        private void OnEnable()
        {
            EventManager.EventManager.OnSoundSettingsUpdate += UpdatePrefs;
            UpdatePrefs();
        }
        private void OnDisable()
        {
            EventManager.EventManager.OnSoundSettingsUpdate -= UpdatePrefs;
        }

        private void UpdatePrefs()
        {
            audioSources[0].mute = Preferences.MuteMusic;
            audioSources[1].mute = Preferences.MuteSFX;
        }

        private void OnValidate()
        {
            audioSources = transform.GetComponentsInChildren<AudioSource>();
        }
    }
}
