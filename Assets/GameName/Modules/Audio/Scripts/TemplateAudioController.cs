
using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace Gamejam2026
{
    /// <summary>
    /// This class is optional. You can call directly to AudioManager from any script
    /// </summary>
    public class TemplateAudioController : MonoBehaviour
    {
        [SerializeField] AudioDataSO _audioDataSO;

        private void Start()
        {
            AudioManager.Instance.SetAudioData(_audioDataSO);
        }
        public void PlayRandomSound()
        {
            int ran = Random.Range(0, 4);
            switch (ran)
            {
                case 0:
                    AudioManager.Instance.PlaySound(TemplateAudioConstants.eating);
                    break;
                case 1:
                    AudioManager.Instance.PlaySound(TemplateAudioConstants.gaining_experience);
                    break;
                case 2:
                    AudioManager.Instance.PlaySound(TemplateAudioConstants.open_chest);
                    break;
                case 3:
                    AudioManager.Instance.PlaySound(TemplateAudioConstants.zombie_roar);
                    break;
            }
        }
        public void PlayRandomMusic()
        {
            int ran = Random.Range(0, 2);
            switch (ran)
            {
                case 0:
                    AudioManager.Instance.PlayMusic(TemplateAudioConstants.it_going_down_now);
                    break;
                case 1:
                    AudioManager.Instance.PlayMusic(TemplateAudioConstants.ryukyuvania);
                    break;
            }
        }

        public void StopMusic()
        {
            AudioManager.Instance.StopMusic();
        }
    }

    /// <summary>
    /// Create a constants class for each Audio Combo
    /// Audio file name must match with Audio name constant
    /// </summary>
    public static class TemplateAudioConstants
    {
        //Music
        public const string it_going_down_now = "it_going_down_now";
        public const string ryukyuvania = "ryukyuvania";

        //Sound
        public const string eating = "eating";
        public const string gaining_experience = "gaining_experience";
        public const string open_chest = "open_chest";
        public const string zombie_roar = "zombie_roar";
    }
}
