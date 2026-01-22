using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using OurUtils;
using UnityEngine.Audio;
using Unity.VisualScripting;
using static Unity.VisualScripting.Member;

namespace Gamejam2026
{
    /// <summary>
    /// Store and manage audios need for a scene
    /// </summary>
    public class AudioManager : SingletonMono<AudioManager>
    {
        [Header("Data")]
        [SerializeField] AudioDataSO _audioDataSO;
        [SerializeField] private List<AudioData> _soundList = new();
        [SerializeField] private List<AudioData> _musicList = new();

        private AudioSource _musicAudioSource;
        List<AudioSource> _availableSoundSource = new();
        List<AudioSource> _busySoundSource = new();

        private void Start()
        {
            _musicAudioSource = gameObject.AddComponent<AudioSource>();
            StartCoroutine(CleanAudioSourceCoroutine());
        }

        public void SetAudioData(AudioDataSO data)
        {
            //TO DO: Clean old data
            _audioDataSO = data;
            _soundList = new(_audioDataSO.SoundList);
            _musicList = new(_audioDataSO.MusicList);

        }


        #region Audio Play
        private float GetSoundFinalVolume(float selfVolume)
        {
            return _soundVolume * _masterVolume * selfVolume;
        }
        private float GetMusicFinalVolume(float selfVolume)
        {
            return _musicVolume * _masterVolume * selfVolume;
        }

        private AudioData FindSoundAudioData(string audioName)
        {
            return _soundList.FirstOrDefault(x => x.AudioName.Equals(audioName));
        }
        private AudioData FindMusicAudioData(string audioName)
        {
            return _musicList.FirstOrDefault(x => x.AudioName.Equals(audioName));
        }

        public void PlaySound(string audioName)
        {
            AudioSource audioSource = null;
            if (_availableSoundSource.Count == 0)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                _busySoundSource.Add(audioSource);
            }
            else
            {
                audioSource = _availableSoundSource[0];
                _availableSoundSource.Remove(audioSource);
                _busySoundSource.Add(audioSource);
            }

            AudioData audioData = FindSoundAudioData(audioName);
            if (audioData != null)
            {
                audioSource.clip = audioData.Clip;
                audioSource.clip.LoadAudioData();
                audioSource.loop = false;
                audioSource.volume = GetSoundFinalVolume(audioData.Volume);
                audioSource.Play();
            }
            else
            {
                Debug.LogError($"Audio {audioName} not found!");
            }
        }

        public void PlayMusic(string audioName)
        {

            AudioData audioData = FindMusicAudioData(audioName);
            if (audioData == null)
            {
                Debug.LogError($"Audio {audioName} not found!");
                return;
            }
            if (_musicAudioSource.isPlaying)
            {
                _musicAudioSource.Stop();
            }

            _musicAudioSource.clip = audioData.Clip;
            _musicAudioSource.clip.LoadAudioData();
            _musicAudioSource.loop = true;
            _musicAudioSource.volume = GetMusicFinalVolume(audioData.Volume);
            _musicAudioSource.Play();
        }

        public void StopMusic()
        {
            _musicAudioSource.Stop();
        }

        private void SetPlayingSoundVolume()
        {
            foreach (var source in _busySoundSource)
            {
                AudioData audioData = FindSoundAudioData(source.clip.name);
                source.volume = GetSoundFinalVolume(audioData.Volume);
            }
        }

        private void SetPlayingMusicVolume()
        {
            if (_musicAudioSource.isPlaying)
            {
                AudioData audioData = FindSoundAudioData(_musicAudioSource.clip.name);
                _musicAudioSource.volume = GetMusicFinalVolume(audioData.Volume);
            }
        }

        IEnumerator CleanAudioSourceCoroutine()
        {
            yield return new WaitForSeconds(1.0f);
            foreach (var source in _busySoundSource.ToList())
            {
                if (!source.isPlaying)
                {

                    _availableSoundSource.Add(source);
                    _busySoundSource.Remove(source);
                }
            }
            StartCoroutine(CleanAudioSourceCoroutine());
        }

        #endregion


        #region Audio Setting
        const string KEY_VIBRATE = "VIBRATE";
        const string KEY_SOUND = "SOUND";
        const string KEY_MUSIC = "MUSIC";
        const string KEY_VIBRATE_STRENGHT = "VIBRATE_STRENGHT";
        const string KEY_SOUND_VOLUME = "SOUND_VOLUME";
        const string KEY_MUSIC_VOLUME = "MUSIC_VOLUME";
        const string KEY_MASTER_VOLUME = "MUSIC_VOLUME";

        [Header("Audio Setting")]
        [SerializeField] private bool _isEnableSound;
        [SerializeField] private bool _isEnableMusic;
        [SerializeField] private bool _isEnableVibrate;

        [Range(0f, 1f)]
        [SerializeField] private float _masterVolume = 0.5f;
        [Range(0f, 1f)]
        [SerializeField] private float _soundVolume = 0.5f;
        [Range(0f, 1f)]
        [SerializeField] private float _musicVolume = 0.5f;
        public bool IsEnableSound
        {
            get
            {
                return _isEnableSound;
            }
            set
            {
                _isEnableSound = value;
                Save(KEY_SOUND, _isEnableSound);
            }
        }

        public bool IsEnableVibrate
        {
            get
            {
                return _isEnableVibrate;
            }
            set
            {
                _isEnableVibrate = value;
                Save(KEY_VIBRATE, _isEnableVibrate);
            }
        }

        public bool IsEnableMusic
        {
            get
            {
                return _isEnableMusic;
            }
            set
            {
                _isEnableMusic = value;
                Save(KEY_MUSIC, _isEnableMusic);
                _musicAudioSource.mute = !value;
                if (_musicAudioSource.mute)
                {
                    _musicAudioSource.Pause();
                }
                else
                {
                    _musicAudioSource.Play();
                }
            }
        }

        public float MasterVolume
        {
            get => _masterVolume;
            set
            {
                _masterVolume = value;
                Save(KEY_MASTER_VOLUME, _masterVolume);
                SetPlayingMusicVolume();
                SetPlayingSoundVolume();
            }
        }
        public float SoundVolume
        {
            get => _soundVolume;
            set
            {
                _soundVolume = value;
                Save(KEY_SOUND_VOLUME, _soundVolume);
                SetPlayingSoundVolume();
            }
        }
        public float MusicVolume
        {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                Save(KEY_MUSIC_VOLUME, _musicVolume);
                SetPlayingMusicVolume();
            }
        }

        public void ToggleSfx()
        {
            IsEnableSound = !_isEnableSound;
        }

        public void ToggleMusic()
        {
            IsEnableMusic = !_isEnableMusic;
        }

        public void ToggleVibrate()
        {
            IsEnableVibrate = !_isEnableVibrate;
        }
        private void Save(string key, bool value)
        {
            UnityEngine.PlayerPrefs.SetInt(key, value ? 1 : 0);
            UnityEngine.PlayerPrefs.Save();
        }
        private void Save(string key, float value)
        {
            UnityEngine.PlayerPrefs.SetFloat(key, value);
            UnityEngine.PlayerPrefs.Save();
        }

        private bool Load(string key, bool defaultValue)
        {
            var value = defaultValue;
            if (UnityEngine.PlayerPrefs.HasKey(key))
            {
                value = UnityEngine.PlayerPrefs.GetInt(key) != 0;
            }
            return value;
        }
        private float Load(string key, float defaultValue)
        {
            var value = defaultValue;
            if (UnityEngine.PlayerPrefs.HasKey(key))
            {
                value = UnityEngine.PlayerPrefs.GetFloat(key);
            }
            return value;
        }
        #endregion


    }


}