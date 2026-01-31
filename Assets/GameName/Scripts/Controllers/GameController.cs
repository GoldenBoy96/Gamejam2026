using System.Collections.Generic;
using System.Threading.Tasks;
using Gamejam2026;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levelPrefabs;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private List<GameObject> _skillPrefab; // Get skill by index
    [SerializeField] AudioDataSO _audioDataSO;
    private bool _isLoadingComplete = false;
    private bool _isPaused = false;
    private bool _isGameOver = false;

    private DataManager _dataManager = new DataManager();
    async void Start()
    {
        AudioManager.Instance.SetAudioData(_audioDataSO);
        for (int i = 0; i < 100; i++)
        {
            await Task.Delay(1000);
            PlayRandomSound();
        }

        _dataManager.LoadPlayerData();
        await LoadLevel(_dataManager.PlayerProgressionData.CurrentLevel);
        _isLoadingComplete = true;
    }

    async Task LoadLevel(int level)
    {
        // TO DO: Load level data from DataManager and initialize level
    }

    public void ResetLevel()
    {

    }

    void Update()
    {
        if (!_isLoadingComplete) return;
        if (_isPaused || _isGameOver) return;

        //TO DO: Call every child entity update
    }

    public void PlayRandomSound()
    {
        int ran = Random.Range(0, 4);
        switch (ran)
        {
            case 0:
                AudioManager.Instance.PlaySound(TemplateAudioConstants.click_sound);
                break;
            case 1:
                AudioManager.Instance.PlaySound(TemplateAudioConstants.gaining_experience);
                break;
            case 2:
                AudioManager.Instance.PlaySound(TemplateAudioConstants.open_chest);
                break;
            case 3:
                AudioManager.Instance.PlaySound(TemplateAudioConstants.zombie_roar);
                break;
        }
    }
}
