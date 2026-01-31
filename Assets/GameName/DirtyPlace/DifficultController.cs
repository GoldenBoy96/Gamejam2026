using UnityEngine;
using UnityEngine.UI;

namespace Gamejam2026
{
    public class DifficultController : MonoBehaviour
    {
        public Button difficult1Button;
        public Button difficult2Button;
        public Button backButton;

        public void StartGameWithDifficult1()
        {
            // UnityEngine.SceneManagement.SceneManager.LoadScene("VisualNovelScene");
            Debug.Log("Bắt đầu trò chơi!");
        }

        public void StartGameWithDifficult2()
        {
            // Logic để bắt đầu trò chơi
            // go to the Difficulty Selection Scene or the main game scene
            Debug.Log("Bắt đầu trò chơi!");
        }

        public void Back()
        {
            // Logic để thoát trò chơi
            UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
            Debug.Log("Thoát trò chơi!");
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            difficult1Button.onClick.AddListener(Back);
            difficult2Button.onClick.AddListener(StartGameWithDifficult2);
            backButton.onClick.AddListener(Back);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
