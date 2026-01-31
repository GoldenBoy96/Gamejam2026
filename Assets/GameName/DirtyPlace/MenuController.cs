using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gamejam2026
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] AudioDataSO _audioDataSO;
        public Button startButton;
        public Button settingsButton;
        public Button exitButton;
        public float delayDuration = 0.3f;

        IEnumerator StartGame() // Tạo một Coroutine mới
        {
            AudioManager.Instance.PlaySound(TemplateAudioConstants.click_sound);
            yield return new WaitForSeconds(delayDuration);
            UnityEngine.SceneManagement.SceneManager.LoadScene("DifficultScene");
            Debug.Log("Bắt đầu trò chơi!");
        }

        IEnumerator OpenSettings()
        {
            AudioManager.Instance.PlaySound(TemplateAudioConstants.click_sound);
            yield return new WaitForSeconds(delayDuration);
            UnityEngine.SceneManagement.SceneManager.LoadScene("DifficultScene");
            Debug.Log("Mở cài đặt!");
        }

        IEnumerator ExitGame()
        {
            AudioManager.Instance.PlaySound(TemplateAudioConstants.click_sound);
            yield return new WaitForSeconds(delayDuration);
            Application.Quit();
            Debug.Log("Thoát trò chơi!");
            yield return null;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            AudioManager.Instance.SetAudioData(_audioDataSO);
            startButton.onClick.AddListener(() => StartCoroutine(StartGame()));
            settingsButton.onClick.AddListener(() => StartCoroutine(OpenSettings()));
            exitButton.onClick.AddListener(() => StartCoroutine(ExitGame()));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}