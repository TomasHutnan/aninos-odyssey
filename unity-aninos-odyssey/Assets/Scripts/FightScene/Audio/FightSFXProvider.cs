using AE.Audio;
using AE.Fight.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioType = AE.Audio.AudioType;

public class FightSFXProvider : MonoBehaviour
{
    public AudioController audioController;

    [System.Serializable]
    public class SFXDescriptor
    {
        public AnimationWeaponClass _class;
        public AudioType[] SFXTracks;
    }

    [SerializeField] SFXDescriptor[] audioDescriptors;
    private Dictionary<AnimationWeaponClass, List<AudioType>> _sfxTracks = new();

    private void Start()
    {
        foreach (SFXDescriptor arr in audioDescriptors)
        {
            if (!_sfxTracks.ContainsKey(arr._class))
                _sfxTracks[arr._class] = new List<AudioType>();

            foreach (AudioType track in arr.SFXTracks)
                _sfxTracks[arr._class].Add(track);
        }
    }

    public void PlaySFX(AnimationWeaponClass Class)
    {
        AudioType toPlay = _sfxTracks[Class][Random.Range(0, _sfxTracks[Class].Count)];
        audioController.PlayAudio(toPlay);
    }
}
