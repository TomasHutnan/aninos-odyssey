using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Audio.PlayControl
{
    public class LocationLocalSTSwitch : MonoBehaviour
    {
        [SerializeField] AudioController audioController;
        [Space]
        [SerializeField] AudioType locatioSoundTrack;
        [SerializeField] AudioType shopSoundTrack = AudioType.ST_SHOP_01;
        [Space]
        [Space]
        [SerializeField] GameObject[] shopGameObjects;

        void Update()
        {
            AudioType currentTrack = locatioSoundTrack;
            foreach (GameObject shop in shopGameObjects)
                if (shop.activeInHierarchy)
                {
                    currentTrack = shopSoundTrack;
                    break;
                }

            if (audioController.IsAudioTrackRunning(currentTrack))
                return;

            audioController.PlayAudio(currentTrack, true);
        }
    }
}
