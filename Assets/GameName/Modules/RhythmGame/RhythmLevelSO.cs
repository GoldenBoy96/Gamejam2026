using System.Collections.Generic;
using UnityEngine;

namespace Gamejam2026
{
    [CreateAssetMenu(fileName = "NewRhythmLevel", menuName = "RhythmGame/LevelData")]
    public class RhythmLevelSO : ScriptableObject
    {
        public string levelName;
        public AudioClip musicClip;
        public float bpm = 128;
        public float noteSpeed;

        [Header("Chart Data")]
        
        
        // Thời điểm (tính bằng Beat) mà người chơi phải bấm trúng
        // Ví dụ: 1.0, 2.0, 3.5, 4.0...
        // Để tạo đoạn im lặng, chỉ cần không điền gì vào khoảng đó.
        public List<float> noteBeats;
    }
}