using OurUtils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Gamejam2026
{
    public class VisualNovelManager : SingletonMono<VisualNovelManager>
    {
        [Header("Config")]
        [SerializeField] private VisualNovelResourcesSO _resources;
        [SerializeField] private VisualNovelUI _ui;

        private Dictionary<string, StoryNode> _nodeMap = new Dictionary<string, StoryNode>();
        private StoryNode _currentNode;
        private int _currentScore = 0;
        private string _currentLevelId;
        public Sprite GetSpecialItem(string id)
        {
            if (_resources == null)
            {
                Debug.LogError("LỖI CỰC LỚN: Bạn chưa gán file 'Test Resources' vào VisualNovelManager!");
                return null;
            }

            Sprite s = _resources.GetSpecialItem(id);

            if (s == null)
            {
                Debug.LogError($"Tìm thấy file Resources, nhưng không tìm thấy item có tên: '{id}'. Hãy kiểm tra lại chính tả ID.");
            }
            else
            {
                Debug.Log($"Đã tìm thấy Item '{id}' thành công! Nếu không hiện thì do lỗi UI.");
            }

            return s;
        }

        // Load from JSON
        public void LoadStory(string jsonFileName)
        {
            string path = Path.Combine(Application.streamingAssetsPath, jsonFileName);

            LevelStoryData storyData = JsonHelper.ReadData<LevelStoryData>(path);

            if (storyData == null)
            {
                Debug.LogError($"Cannot load story at {path}");
                return;
            }

            _currentLevelId = storyData.levelId;

            // List to Dictionary for search
            _nodeMap.Clear();
            foreach (var node in storyData.nodes)
            {
                if (!_nodeMap.ContainsKey(node.nodeId))
                    _nodeMap.Add(node.nodeId, node);
            }

            // Check file save xem có đang chơi dở không
            // Giả sử DataManager được truy cập qua GameController hoặc Singleton (nếu DataManager ko phải Singleton thì cần truyền reference)
            // Ở đây tôi giả định bạn tạo instance DataManager ở đâu đó, ví dụ trong GameController.
            // Để đơn giản cho ví dụ này, tôi sẽ check Node bắt đầu từ StoryData

            // Logic: Nếu có save -> Load node từ save. Nếu không -> Load startNodeId
            string startNode = storyData.startNodeId;
            // TODO: Kết nối với DataManager.VNSaveData ở đây để override startNode

            PlayNode(startNode);
        }

        private void PlayNode(string nodeId)
        {
            if (string.IsNullOrEmpty(nodeId) || !_nodeMap.ContainsKey(nodeId))
            {
                Debug.Log("End of story or Invalid Node: " + nodeId);
                _ui.ToggleUIVisibility(false);
                return;
            }

            _currentNode = _nodeMap[nodeId];

            // Auto Save tiến trình
            //FindObjectOfType<GameController>().SaveVNProgress(_currentLevelId, nodeId, _currentScore); 

            // Gửi dữ liệu sang UI để hiển thị
            _ui.DisplayNode(_currentNode, OnDialogueFinished);
        }

        // Callback khi UI chạy hết các dòng thoại của Node hiện tại
        private void OnDialogueFinished()
        {
            // Kiểm tra xem có lựa chọn (Options) hay không
            if (_currentNode.options != null && _currentNode.options.Count > 0)
            {
                _ui.ShowOptions(_currentNode.options, OnOptionSelected);
            }
            else
            {
                // Không có lựa chọn -> Tự động chuyển sang node tiếp theo
                PlayNode(_currentNode.nextNodeId);
            }
        }

        private void OnOptionSelected(Option option)
        {
            // Cộng điểm
            _currentScore += option.scoreDelta;
            Debug.Log($"Selected: {option.text}. Current Score: {_currentScore}");

            // Chuyển node
            PlayNode(option.nextNodeId);
        }

        public Sprite GetBackground(string id) => _resources.GetBackground(id);
        public Sprite GetCharacter(string id, string face) => _resources.GetCharacterSprite(id, face);
    }
}