using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gamejam2026
{
    public class DifficultController : MonoBehaviour
    {
        [SerializeField] AudioDataSO _audioDataSO;
        public Button difficult1Button;
        public Button difficult2Button;
        public Button backButton;
        public float delayDuration = 0.3f;


        IEnumerator StartGameWithDifficult1()
        {
            AudioManager.Instance.PlaySound(TemplateAudioConstants.click_sound);
            yield return new WaitForSeconds(delayDuration);
            UnityEngine.SceneManagement.SceneManager.LoadScene("SigmaScene1");
            Debug.Log("Bắt đầu trò chơi!");
        }

        IEnumerator StartGameWithDifficult2()
        {
            AudioManager.Instance.PlaySound(TemplateAudioConstants.click_sound);
            yield return new WaitForSeconds(delayDuration);
            UnityEngine.SceneManagement.SceneManager.LoadScene("SigmaScene2");
            Debug.Log("Bắt đầu trò chơi!");
        }

        IEnumerator Back()
        {
            AudioManager.Instance.PlaySound(TemplateAudioConstants.click_sound);
            yield return new WaitForSeconds(delayDuration);
            UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
            Debug.Log("Thoát trò chơi!");
        }

        void Start()
        {
            AudioManager.Instance.SetAudioData(_audioDataSO);
            difficult1Button.onClick.AddListener(() => StartCoroutine(StartGameWithDifficult1()));
            difficult2Button.onClick.AddListener(() => StartCoroutine(StartGameWithDifficult2()));
            backButton.onClick.AddListener(() => StartCoroutine(Back()));
        }

        void Update()
        {

        }
    }
}
