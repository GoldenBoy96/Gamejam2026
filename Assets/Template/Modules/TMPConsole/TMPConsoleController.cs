using OurUtils;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SushiGame2048
{
    public class TMPConsoleController : SingletonMono<TMPConsoleController>
    {
        [SerializeField] private TMP_Text _logText;
        [SerializeField] private ScrollRect _scrollRect;

        private void Start()
        {
            if (_logText == null)
                Debug.LogError("LogText is not assigned!");

            if (_scrollRect == null)
                Debug.LogError("Scrollbar is not assigned!");
        }

        public void Log(string message, GameObject sender)
        {
            // Append the new message
            string timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            message = $"\n[{timestamp} {sender.name}]\n" + message;
            _logText.text += message + "\n";

            // Force the Canvas to rebuild to update the Scrollbar
            Canvas.ForceUpdateCanvases();

            // Scroll to the bottom
            _scrollRect.verticalNormalizedPosition = 0f;
        }
        public void LogError(string message, GameObject sender)
        {
            // Append the new message
            string timestamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            message = $"\n[{timestamp} {sender.name}]\n" + message;
            _logText.text += $"<color=red>{message}</color>" + "\n";

            // Force the Canvas to rebuild to update the Scrollbar
            Canvas.ForceUpdateCanvases();

            // Scroll to the bottom
            _scrollRect.verticalNormalizedPosition = 0f;
        }

        public void Input(string message, GameObject owner, Action onSuccess, Action onError)
        {

        }
    }
}