using AE.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE.Audio.Fight
{
    public class FightSoundTrackProvider : MonoBehaviour
    {
        [System.Serializable]
        public class AudioInfo
        {
            public AudioType[] AudioTracks;
            public Location FightLocation;
            public FightType FightType;
        }

        [SerializeField] AudioController audioController;
        [SerializeField] AudioInfo[] audioInfos;

        private Dictionary<Location, Dictionary<FightType, AudioType[]>> _soundTracks = new();

        private int lastPlayedIndex = -1;

        private void Awake()
        {
            foreach (AudioInfo audioInfo in audioInfos)
            {
                if (!_soundTracks.ContainsKey(audioInfo.FightLocation))
                    _soundTracks[audioInfo.FightLocation] = new Dictionary<FightType, AudioType[]>();

                _soundTracks[audioInfo.FightLocation][audioInfo.FightType] = audioInfo.AudioTracks;
            }

            if (FightData.FightType == FightType.None)
                FightData.FightType = FightType.Normal;
            if (FightData.Location == Location.None)
                FightData.Location = Location.Greece;
        }

        private void Update()
        {
            AudioType[] soundTracks = _soundTracks[FightData.Location][FightData.FightType];
            if (lastPlayedIndex == -1)
            {
                lastPlayedIndex = 0;
                audioController.PlayAudio(soundTracks[lastPlayedIndex], true);
                return;
            }

            if (audioController.IsAudioTrackRunning(soundTracks[lastPlayedIndex]))
                return;

            if (lastPlayedIndex < soundTracks.Length - 1)
                lastPlayedIndex++;

            audioController.PlayAudio(soundTracks[lastPlayedIndex], false);
        }
    }
}
