using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamejam2026
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData", order = 0)]
    [Serializable]
    public class AudioDataSO : ScriptableObject
    {
        [SerializeField] private List<AudioData> _soundList = new();
        [SerializeField] private List<AudioData> _musicList = new();

        public List<AudioData> SoundList { get => _soundList; set => _soundList = value; }
        public List<AudioData> MusicList { get => _musicList; set => _musicList = value; }
    }
}