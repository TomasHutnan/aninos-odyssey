using AE.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioType = AE.Audio.AudioType;

namespace AE.MainMenu
{
    public class SFXProvider : MonoBehaviour
    {
        [SerializeField] AudioController audioController;
        [Space]
        //[SerializeField] AudioType buttonHoverSFX;
        [SerializeField] AudioType buttonPressedSFX;

        //public void OnButtonHover()
        //{
        //    if (buttonHoverSFX != AudioType.None)
        //        audioController.PlayAudio(buttonHoverSFX);
        //}
        public void OnButtonPressed()
        {
            if (buttonPressedSFX != AudioType.None)
                audioController.PlayAudio(buttonPressedSFX);
        }
    }
}
