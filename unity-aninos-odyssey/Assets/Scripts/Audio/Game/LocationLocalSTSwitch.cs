using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Audio.PlayControl
{
    public class LocationLocalSTSwitch : MonoBehaviour
    {
        [SerializeField] AudioController audioController;
        [Space]
        [SerializeField] AudioType[] locatioSoundTrack;
        [SerializeField] AudioType[] shopSoundTrack = new AudioType[] { AudioType.OST_SHOP_INTRO, AudioType.OST_SHOP_MAIN };
        [Space]
        [Space]
        [SerializeField] GameObject[] shopGameObjects;

        private AudioType lastPlayed = AudioType.OST_SHOP_MAIN;

        void Update()
        {
            AudioType currentTrack = lastPlayed == AudioType.OST_SHOP_INTRO || lastPlayed == AudioType.OST_SHOP_MAIN ? locatioSoundTrack[0] : locatioSoundTrack[1];
            
            foreach (GameObject shop in shopGameObjects)
                if (shop.activeInHierarchy)
                {
                    if (audioController.IsAudioTrackRunning(shopSoundTrack[0]))
                        return;
                    if (audioController.IsAudioTrackRunning(shopSoundTrack[1]))
                        return;
                    currentTrack = lastPlayed == locatioSoundTrack[0] || lastPlayed == locatioSoundTrack[1] ? shopSoundTrack[0] : shopSoundTrack[1];
                    break;
                }

            if (currentTrack == locatioSoundTrack[1] && audioController.IsAudioTrackRunning(locatioSoundTrack[0]))
                return;

            if (audioController.IsAudioTrackRunning(currentTrack))
                return;

            lastPlayed = currentTrack;
            audioController.PlayAudio(currentTrack);
        }
    }
}
