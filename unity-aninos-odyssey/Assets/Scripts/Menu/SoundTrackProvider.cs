using AE.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioType = AE.Audio.AudioType;

namespace AE.MainMenu
{
    public class SoundTrackProvider : MonoBehaviour
    {
        [SerializeField] AudioController audioController;
        [Space]
        [SerializeField] AudioType menuSoundTrack;

        // Update is called once per frame
        void Update()
        {
            if (!audioController.IsAudioTrackRunning(menuSoundTrack))
                audioController.PlayAudio(menuSoundTrack);
        }
    }
}
