using OurUtils;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Gamejam2026
{
    public class RhythmGameManager : SingletonMono<RhythmGameManager>
    {
        [Header("Components")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Transform _noteHolder;
        [SerializeField] private RhythmNote _notePrefab;

        // --- SỬA ĐỔI 1: Thay vì 1 điểm, ta dùng 1 List điểm ---
        [SerializeField] private List<RectTransform> _spawnPoints;

        [SerializeField] private RectTransform _targetPoint;
        [SerializeField] private TextMeshProUGUI _feedbackText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        [Header("Settings")]
        [SerializeField] private RhythmLevelSO _currentLevel;
        [SerializeField] private float _hitWindow = 0.15f;

        // --- SỬA ĐỔI 2: Mục tiêu chiến thắng ---
        [SerializeField] private int _targetScoreToWin = 12;

        [Header("Game State")]
        [SerializeField] private int _maxHealth = 3;
        private int _currentHealth;
        private int _currentScore = 0; // Biến đếm điểm
        private float _secPerBeat;
        private float _dspSongTime;
        private float _beatsPrespawn; // Tính toán tự động
        private bool _isPlaying = false;
        private bool _isGameFinished = false;

        private int _nextIndexToSpawn = 0;
        private List<RhythmNote> _activeNotes = new List<RhythmNote>();

        private void Start()
        {
            // Tự động chạy nếu có sẵn Level trong Inspector
            if (_currentLevel != null)
            {
                StartGame(_currentLevel);
            }
            else
            {
                Debug.LogWarning("Chưa gán LevelSO vào Manager nên game không tự chạy!");
            }
        }

        public void StartGame(RhythmLevelSO level)
        {
            _currentLevel = level;
            _secPerBeat = 60f / level.bpm;
            _currentHealth = _maxHealth;
            _currentScore = 0;
            _nextIndexToSpawn = 0;
            _isGameFinished = false;
            _activeNotes.Clear();
            UpdateUI();

            if (_spawnPoints.Count > 0)
            {
                float distance = Vector2.Distance(_spawnPoints[0].anchoredPosition, _targetPoint.anchoredPosition);
                float speed = level.noteSpeed > 0 ? level.noteSpeed : 500f;
                float timeOnScreen = distance / speed;
                _beatsPrespawn = timeOnScreen / _secPerBeat;
            }
            else
            {
                _beatsPrespawn = 4f; 
            }

            foreach (Transform child in _noteHolder) Destroy(child.gameObject);

            _audioSource.clip = level.musicClip;
            _audioSource.PlayDelayed(1.0f);
            _dspSongTime = (float)AudioSettings.dspTime + 1.0f;
            _isPlaying = true;

            Debug.Log($"Bắt đầu game! Cần đạt {_targetScoreToWin} điểm để thắng.");
        }

        private void Update()
        {
            if (!_isPlaying || _isGameFinished) return;

            float songPosition = (float)(AudioSettings.dspTime - _dspSongTime);
            float songPositionInBeats = songPosition / _secPerBeat;

            // 1. Spawn Note (Chọn ngẫu nhiên 1 trong 4 góc)
            if (_nextIndexToSpawn < _currentLevel.noteBeats.Count)
            {
                float nextBeat = _currentLevel.noteBeats[_nextIndexToSpawn];
                if (nextBeat - _beatsPrespawn <= songPositionInBeats)
                {
                    SpawnNote(nextBeat);
                    _nextIndexToSpawn++;
                }
            }

            // 2. Update Notes
            for (int i = _activeNotes.Count - 1; i >= 0; i--)
            {
                RhythmNote note = _activeNotes[i];
                note.ManualUpdate(songPositionInBeats);

                if (note.IsMissed)
                {
                    HandleMiss();
                    Destroy(note.gameObject);
                    _activeNotes.RemoveAt(i);
                }
            }

            // 3. Input
            if (Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                ProcessInput(songPosition);
            }

            // 4. Kiểm tra Kết Thúc (Hết nhạc và hết nốt)
            bool finishedSpawning = _nextIndexToSpawn >= _currentLevel.noteBeats.Count;
            bool noActiveNotes = _activeNotes.Count == 0;

            // Thêm điều kiện: Nhạc đã chạy hết hoặc đã spawn hết nốt được một lúc
            bool musicEnded = !_audioSource.isPlaying || songPosition > _audioSource.clip.length;

            if (finishedSpawning && noActiveNotes && musicEnded)
            {
                FinishGame();
            }
        }

        private void SpawnNote(float beat)
        {
            // --- LOGIC MỚI: Random vị trí ---
            RectTransform randomSpawnPoint;
            if (_spawnPoints.Count > 0)
            {
                int randomIndex = Random.Range(0, _spawnPoints.Count);
                randomSpawnPoint = _spawnPoints[randomIndex];
            }
            else
            {
                Debug.LogError("Chưa gán Spawn Points vào List!");
                return;
            }

            RhythmNote newNote = Instantiate(_notePrefab, _noteHolder);
            // Setup với vị trí xuất phát ngẫu nhiên
            newNote.Setup(beat, _beatsPrespawn, _secPerBeat, randomSpawnPoint.anchoredPosition, _targetPoint.anchoredPosition);

            // Xoay nốt nhạc hướng về tâm (Optional - Để mũi tên quay đúng hướng)
            Vector3 dir = _targetPoint.anchoredPosition - randomSpawnPoint.anchoredPosition;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            newNote.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            _activeNotes.Add(newNote);
        }

        private void ProcessInput(float songPosition)
        {
            RhythmNote hitNote = null;
            float minDiff = float.MaxValue;

            foreach (var note in _activeNotes)
            {
                float diff = Mathf.Abs(note.targetBeat * _secPerBeat - songPosition);
                if (diff < minDiff)
                {
                    minDiff = diff;
                    hitNote = note;
                }
            }

            if (hitNote != null && minDiff <= _hitWindow)
            {
                // HIT! -> Cộng điểm
                _currentScore++;
                ShowFeedback("Perfect!", Color.green);
                Destroy(hitNote.gameObject);
                _activeNotes.Remove(hitNote);
                UpdateUI();
                if (_currentScore >= _targetScoreToWin)
                {
                    Victory();
                }
            }
            else
            {
                ShowFeedback("Don't Panic!", Color.red);
                TakeDamage();
            }
        }

        private void HandleMiss()
        {
            ShowFeedback("Miss!", Color.red);
            TakeDamage();
        }

        private void TakeDamage()
        {
            _currentHealth--;
            UpdateUI();
            if (_currentHealth <= 0)
            {
                GameOver();
            }
        }

        private void UpdateUI()
        {
            if (_healthText) _healthText.text = $"HP: {_currentHealth}";
            if (_scoreText) _scoreText.text = $"Score: {_currentScore}/{_targetScoreToWin}";
        }

        private void ShowFeedback(string text, Color color)
        {
            if (_feedbackText)
            {
                _feedbackText.text = text;
                _feedbackText.color = color;
                CancelInvoke(nameof(ClearFeedback));
                Invoke(nameof(ClearFeedback), 0.5f);
            }
        }
        private void ClearFeedback() => _feedbackText.text = "";

        private void FinishGame()
        {
            _isGameFinished = true;
            _isPlaying = false;
            _audioSource.Stop();

            // --- LOGIC MỚI: Kiểm tra điểm số ---
            if (_currentScore >= _targetScoreToWin)
            {
                Victory();
            }
            else
            {
                Debug.Log($"Thất bại! Chỉ đạt {_currentScore} điểm.");
                ShowFeedback("FAILED...", Color.gray);
                // Xử lý logic thua (Load lại màn hoặc Bad End)
            }
        }

        private void GameOver()
        {
            _isGameFinished = true;
            _isPlaying = false;
            _audioSource.Stop();
            ShowFeedback("GAME OVER", Color.red);
            // Xử lý Bad End
        }

        private void Victory()
        {
            // 1. Dừng mọi hoạt động game
            _isGameFinished = true;
            _isPlaying = false;

            // 2. Tắt nhạc ngay lập tức
            _audioSource.Stop();

            // 3. (Tùy chọn) Xóa sạch các nốt còn lại trên màn hình cho đẹp
            foreach (var note in _activeNotes)
            {
                if (note != null) Destroy(note.gameObject);
            }
            _activeNotes.Clear();

            // 4. Thông báo
            Debug.Log("GOOD END: Đủ điểm! Chiến thắng!");
            ShowFeedback("VICTORY!!", Color.yellow);

            // TODO: Gọi lệnh chuyển Level hoặc hiện bảng Win tại đây
        }
    }
}