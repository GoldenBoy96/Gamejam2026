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

        [Header("Special Item Display")]
        [SerializeField] private Image _specialItemImage;

        [Header("Dialogue UI")]
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Transform _optionsParent;
        [SerializeField] private Button _optionButtonPrefab;

        [Header("Text Styles")]
        [SerializeField] private Color _normalTextColor = Color.black;
        [SerializeField] private Color _thoughtTextColor = new Color(0.8f, 0.8f, 1f, 1f);
        [SerializeField] private TMP_FontAsset _normalFont;
        [SerializeField] private TMP_FontAsset _thoughtFont;

        private bool _isTyping = false;
        private bool _skipRequested = false;
        private int _currentTypingId = 0;

        public void ToggleUIVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
            // Reset trạng thái khi tắt/bật UI
            if (isVisible && _specialItemImage != null) _specialItemImage.gameObject.SetActive(false);
        }

        public async void DisplayNode(StoryNode node, Action onFinished)
        {
            _currentTypingId++;
            int myId = _currentTypingId;

            ToggleUIVisibility(true);
            foreach (Transform child in _optionsParent) Destroy(child.gameObject);

            foreach (var line in node.dialogueLines)
            {
                if (myId != _currentTypingId) return;

                // 1. Cập nhật Visual (Nhân vật, Nền) TRƯỚC KHI xử lý lệnh
                // Điều này đảm bảo nhân vật không bị mất dù dòng đó là lệnh
                UpdateVisuals(line);
                ApplyTextStyle(line.characterId);

                // 2. Xử lý lệnh đặc biệt !!
                if (line.text.StartsWith("!!"))
                {
                    bool success = HandleSpecialCommand(line.text);
                    if (success)
                    {
                        // Nếu hiện item thành công, đợi 1.5s để người chơi nhìn
                        await WaitForInputNext(myId);
                    }
                    else
                    {
                        // Nếu lỗi (không tìm thấy ảnh), bỏ qua nhanh
                        await Task.Delay(100);
                    }
                    continue; // Bỏ qua việc chạy chữ, sang dòng tiếp theo luôn
                }

                // 3. Nếu là thoại thường -> Phải ẩn Item đi (Fix lỗi item không ẩn)
                if (_specialItemImage != null) _specialItemImage.gameObject.SetActive(false);

                // 4. Chạy chữ
                await PlayDialogueSequence(line.text, myId);

                if (myId != _currentTypingId) return;

                // 5. Đợi click
                await WaitForInputNext(myId);
            }

            if (myId == _currentTypingId)
            {
                onFinished?.Invoke();
            }
        }

        private bool HandleSpecialCommand(string commandText)
        {
            // Cấu trúc: !!ITEM_ID
            string itemId = commandText.Substring(2).Trim();

            Sprite itemSprite = VisualNovelManager.Instance.GetSpecialItem(itemId);

            if (_specialItemImage != null && itemSprite != null)
            {
                _specialItemImage.sprite = itemSprite;
                _specialItemImage.gameObject.SetActive(true); // Bật ảnh lên
                _specialItemImage.preserveAspect = true; // Giữ tỉ lệ ảnh không bị méo

                // Xóa text để tập trung vào item
                _dialogueText.text = "";
                _nameText.text = "";
                return true;
            }
            else
            {
                Debug.LogWarning($"[VisualNovelUI] Không tìm thấy item ID '{itemId}' trong ResourcesSO hoặc chưa gán Image vào Inspector.");
                return false;
            }
        }

        private void UpdateVisuals(DialogueLine line)
        {
            var bgSprite = VisualNovelManager.Instance.GetBackground(line.backgroundId);

            // Lấy ảnh nhân vật. Nếu id rỗng, giữ nguyên ảnh cũ (để không bị mất nhân vật khi hiện item)
            // Trừ khi bạn muốn ẩn nhân vật thì cần gán id="none" hoặc logic riêng.
            Sprite charSprite = null;
            if (!string.IsNullOrEmpty(line.characterId))
            {
                charSprite = VisualNovelManager.Instance.GetCharacter(line.characterId, line.faceId);
            }

            if (bgSprite != null) _backgroundImage.sprite = bgSprite;

            // Logic hiển thị nhân vật:
            // Chỉ cập nhật nếu tìm thấy sprite mới. 
            // Nếu line.characterId rỗng (ví dụ dòng lệnh), ta KHÔNG làm gì cả -> Giữ nguyên nhân vật đang đứng đó.
            if (charSprite != null)
            {
                _characterImage.gameObject.SetActive(true);
                _characterImage.sprite = charSprite;
                _characterImage.preserveAspect = true;
            }
            //else if (line.characterId == "narrator" || line.characterId == "player")
            //{
            //    // Nếu là người dẫn chuyện hoặc player suy nghĩ -> Ẩn ảnh nhân vật đối diện
            //    _characterImage.gameObject.SetActive(false);
            //}
        }

        // ... (Giữ nguyên các hàm ApplyTextStyle, PlayDialogueSequence, WaitForInputNext, OnClickScreen, ShowOptions như cũ)
        private void ApplyTextStyle(string characterId)
        {
            if (characterId.ToLower() == "player")
            {
                _dialogueText.fontStyle = FontStyles.Italic;
                _dialogueText.color = _thoughtTextColor;
                if (_thoughtFont) _dialogueText.font = _thoughtFont;
                _nameText.text = "";
            }
            else
            {
                _dialogueText.fontStyle = FontStyles.Normal;
                _dialogueText.color = _normalTextColor;
                if (_normalFont) _dialogueText.font = _normalFont;
                _nameText.text = characterId;
            }
        }

        public async Task PlayDialogueSequence(string dialogueLine, int checkId)
        {
            _isTyping = true;
            _skipRequested = false;
            _dialogueText.text = "";
            char[] chars = dialogueLine.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (checkId != _currentTypingId) return;
                if (_skipRequested || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    _dialogueText.text = dialogueLine;
                    await Task.Delay(100);
                    break;
                }
                _dialogueText.text += chars[i];
                await Task.Delay(20);
            }
            _isTyping = false;
            _skipRequested = false;
        }

        private async Task WaitForInputNext(int checkId)
        {
            while (!Input.GetMouseButtonDown(0) && !Input.GetKeyDown(KeyCode.Space))
            {
                if (checkId != _currentTypingId) return;
                await Task.Yield();
            }
            await Task.Delay(100);
        }

        public void OnClickScreen()
        {
            if (_isTyping) _skipRequested = true;
        }

        public void ShowOptions(List<Option> options, Action<Option> onSelected)
        {
            if (_optionButtonPrefab == null) return;
            foreach (var opt in options)
            {
                Button btn = Instantiate(_optionButtonPrefab, _optionsParent);
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