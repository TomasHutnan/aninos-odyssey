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

        private Dictionary<Location, Dictionary<FightType, List<AudioType[]>>> _soundTracks = new();

        private int lastPlayedIndex = -1;

        private AudioType[] soundTracks;

        private void Awake()
        {
            foreach (AudioInfo audioInfo in audioInfos)
            {
                if (!_soundTracks.ContainsKey(audioInfo.FightLocation))
                    _soundTracks[audioInfo.FightLocation] = new Dictionary<FightType, List<AudioType[]>>();

                if (!_soundTracks[audioInfo.FightLocation].ContainsKey(audioInfo.FightType))
                    _soundTracks[audioInfo.FightLocation][audioInfo.FightType] = new List<AudioType[]>();

                _soundTracks[audioInfo.FightLocation][audioInfo.FightType].Add(audioInfo.AudioTracks);
            }

            if (FightData.FightType == FightType.None)
                FightData.FightType = FightType.Normal;
            if (FightData.Location == Location.None)
                FightData.Location = Location.Greece;
        }

        private void Start()
        {
            soundTracks = _soundTracks[FightData.Location][FightData.FightType][Random.Range(0, _soundTracks[FightData.Location][FightData.FightType].Count)];
        }

        private void Update()
        {
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
