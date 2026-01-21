using OurUtils;
using System;
using System.Collections;
using UnityEngine;

namespace GameName
{
    [Serializable]
    public class AudioData : ICloneable<AudioData>
    {
        [SerializeField] private AudioClip _clip;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _volume;
        public AudioClip Clip { get => _clip; set => _clip = value; }
        public float Volume { get => _volume; set => _volume = value; }
        public string AudioName { get => _clip.name;}

        public override string ToString()
        {
            return $"{{{nameof(Clip)}={Clip}, {nameof(Volume)}={Volume.ToString()}, {nameof(AudioName)}={AudioName}}}";
        }
    }

}