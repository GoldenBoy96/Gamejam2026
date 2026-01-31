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
            UnityEngine.SceneManagement.SceneManager.LoadScene("SigmaScene");
            Debug.Log("Bắt đầu trò chơi!");
        }

        public void StartGameWithDifficult2()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SigmaScene");
            Debug.Log("Bắt đầu trò chơi!");
        }

        public void Back()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
            Debug.Log("Thoát trò chơi!");
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            difficult1Button.onClick.AddListener(StartGameWithDifficult1);
            difficult2Button.onClick.AddListener(StartGameWithDifficult2);
            backButton.onClick.AddListener(Back);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
