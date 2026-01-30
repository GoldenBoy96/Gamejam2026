using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gamejam2026
{
    public class VisualNovelUI : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _characterImage;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Transform _optionsParent;
        [SerializeField] private Button _optionButtonPrefab; // Đảm bảo đã kéo Prefab vào đây

        private bool _isTyping = false;
        private bool _skipRequested = false;
        private int _currentTypingId = 0; // ID để kiểm soát luồng typing

        public void ToggleUIVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }

        public async void DisplayNode(StoryNode node, Action onFinished)
        {
            // Tăng ID lên để hủy tất cả các tiến trình cũ đang chạy dở
            _currentTypingId++;
            int myId = _currentTypingId;

            ToggleUIVisibility(true);

            // Xóa sạch các nút lựa chọn cũ ngay lập tức
            foreach (Transform child in _optionsParent) Destroy(child.gameObject);

            // Chạy qua từng dòng thoại
            foreach (var line in node.dialogueLines)
            {
                // Nếu ID đã thay đổi (tức là có một lệnh DisplayNode mới được gọi), dừng ngay lập tức
                if (myId != _currentTypingId) return;

                UpdateVisuals(line);

                // Chạy chữ và đợi
                await PlayDialogueSequence(line.text, myId);

                // Nếu ID thay đổi trong lúc đang chạy chữ, dừng lại
                if (myId != _currentTypingId) return;

                await WaitForInput(myId);
            }

            // Chỉ gọi callback khi đây vẫn là tiến trình mới nhất
            if (myId == _currentTypingId)
            {
                onFinished?.Invoke();
            }
        }

        private void UpdateVisuals(DialogueLine line)
        {
            var bgSprite = VisualNovelManager.Instance.GetBackground(line.backgroundId);
            var charSprite = VisualNovelManager.Instance.GetCharacter(line.characterId, line.faceId);

            if (bgSprite != null) _backgroundImage.sprite = bgSprite;

            if (charSprite != null)
            {
                _characterImage.gameObject.SetActive(true);
                _characterImage.sprite = charSprite;
            }
            else
            {
                if (string.IsNullOrEmpty(line.characterId))
                    _characterImage.gameObject.SetActive(false);
            }

            _nameText.text = string.IsNullOrEmpty(line.characterId) ? "" : line.characterId;
        }

        public async Task PlayDialogueSequence(string dialogueLine, int checkId)
        {
            _isTyping = true;
            _skipRequested = false;
            _dialogueText.text = "";

            foreach (char letter in dialogueLine.ToCharArray())
            {
                // Kiểm tra nếu luồng này đã cũ -> thoát
                if (checkId != _currentTypingId) return;

                if (_skipRequested)
                {
                    _dialogueText.text = dialogueLine;
                    break;
                }
                _dialogueText.text += letter;
                await Task.Delay(30);
            }
            _isTyping = false;
        }

        private async Task WaitForInput(int checkId)
        {
            while (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.Space))
            {
                // Kiểm tra liên tục, nếu luồng cũ -> thoát loop
                if (checkId != _currentTypingId) return;
                await Task.Yield();
            }
            // Delay nhẹ chống double click
            await Task.Delay(100);
        }

        public void OnClickScreen()
        {
            if (_isTyping)
            {
                _skipRequested = true;
            }
        }

        public void ShowOptions(List<Option> options, Action<Option> onSelected)
        {
            // Kiểm tra null để tránh lỗi đỏ
            if (_optionButtonPrefab == null)
            {
                Debug.LogError("CHƯA GẮN PREFAB CHO NÚT LỰA CHỌN TRONG INSPECTOR!");
                return;
            }

            foreach (var opt in options)
            {
                Button btn = Instantiate(_optionButtonPrefab, _optionsParent);
                // Tìm text bên trong button (hỗ trợ cả TMP và Text thường)
                var tmpText = btn.GetComponentInChildren<TextMeshProUGUI>();
                if (tmpText != null) tmpText.text = opt.text;

                btn.onClick.AddListener(() =>
                {
                    foreach (Transform child in _optionsParent) Destroy(child.gameObject);
                    onSelected?.Invoke(opt);
                });
            }
        }
    }
}