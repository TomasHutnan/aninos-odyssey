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
            EventManager.EventManager.OnSoundSettingsUpdate += UpdatePrefs;
        }

        private void UpdatePrefs()
        {
            for (int i = 0; i < audioSources.Length; i++)
                audioSources[i].mute = SaveData.IsMuted[i];
        }

        private void OnValidate()
        {
            audioSources = transform.GetComponentsInChildren<AudioSource>();
        }
    }
}
