using UnityEngine;
using UnityEngine.UI;

public class TryAgainController : MonoBehaviour
{
    public Button tryAgainButton;
    public Button mainMenuButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tryAgainButton.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MiniGameScene");
            Debug.Log("Thử lại trò chơi!");
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
            Debug.Log("Quay về menu chính!");
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
