using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gamejam2026
{
    public class EndingController : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI _endingTitle;
        [SerializeField] private TextMeshProUGUI _endingDescription;
        [SerializeField] private Button _menuButton;

        [Header("Content")]
        [TextArea] public string titleText;
        [TextArea] public string descriptionText;

        void Start()
        {
            if (_endingTitle != null) _endingTitle.text = titleText;
            if (_endingDescription != null) _endingDescription.text = descriptionText;

            if (_menuButton != null)
            {
                _menuButton.onClick.AddListener(BackToMenu);
            }
        }

        public void BackToMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
        }
    }
}