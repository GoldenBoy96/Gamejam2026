using UnityEngine;
using UnityEngine.UI;

public class TryAgainController : MonoBehaviour
{
    public Button tryAgainButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tryAgainButton.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MiniGameScene");
            Debug.Log("Thử lại trò chơi!");
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
