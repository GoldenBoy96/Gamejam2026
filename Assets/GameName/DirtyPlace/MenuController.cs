using UnityEngine;
using UnityEngine.UI;

namespace Gamejam2026
{
    public class MenuController : MonoBehaviour
    {
        public Button startButton;
        public Button settingsButton;
        public Button exitButton;

        public void StartGame()
        {
            // Logic để bắt đầu trò chơi
            // go to the Difficulty Selection Scene or the main game scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("DifficultScene");
            Debug.Log("Bắt đầu trò chơi!");
        }

        public void OpenSettings()
        {
            // Logic để mở menu cài đặt
            Debug.Log("Mở cài đặt!");
        }

        public void ExitGame()
        {
            // Logic để thoát trò chơi
            Debug.Log("Thoát trò chơi!");
            Application.Quit();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            startButton.onClick.AddListener(StartGame);
            settingsButton.onClick.AddListener(OpenSettings);
            exitButton.onClick.AddListener(ExitGame);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}