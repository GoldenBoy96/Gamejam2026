using UnityEngine;
using UnityEngine.UI;

namespace Gamejam2026
{
    public class RhythmNote : MonoBehaviour
    {
        public float targetBeat; // Beat mục tiêu cần bấm trúng
        private float _spawnBeat;
        private float _beatDuration; // 1 beat mất bao nhiêu giây
        private Vector2 _startPos;
        private Vector2 _endPos;
        private RectTransform _rectTransform;
        private bool _isMissed = false;

        public bool IsMissed => _isMissed;

        public void Setup(float targetBeat, float beatsPrespawn, float beatDuration, Vector2 startPos, Vector2 endPos)
        {
            _rectTransform = GetComponent<RectTransform>();
            this.targetBeat = targetBeat;
            this._beatDuration = beatDuration;
            this._startPos = startPos;
            this._endPos = endPos;

            // Tính toán nốt này sinh ra ở beat nào
            _spawnBeat = targetBeat - beatsPrespawn;

            // Đặt vị trí ban đầu
            _rectTransform.anchoredPosition = startPos;
        }

        public void ManualUpdate(float currentSongBeat)
        {
            // Tính toán tiến độ di chuyển (0 là ở chỗ spawn, 1 là ở đích)
            // Công thức: (Beat hiện tại - Beat sinh ra) / (Tổng số beat để bay đến đích)
            float progress = (currentSongBeat - _spawnBeat) / (targetBeat - _spawnBeat);

            _rectTransform.anchoredPosition = Vector2.LerpUnclamped(_startPos, _endPos, progress);

            // Nếu đi quá đích một đoạn mà chưa bị destroy -> Miss
            if (progress > 1.2f && !_isMissed)
            {
                _isMissed = true;
                // Báo cho Manager biết là đã trượt (nếu cần xử lý visual ở đây)
            }
        }
    }
}