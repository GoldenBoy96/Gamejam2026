using System.Threading.Tasks;
using Gamejam2026;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private bool _isLoadingComplete = false;
    private bool _isPaused = false;
    private bool _isGameOver = false;

    private DataManager _dataManager = new DataManager();
    async void Start()
    {
        _dataManager.LoadPlayerData();        
        await LoadLevel(_dataManager.PlayerProgressionData.CurrentLevel);
        _isLoadingComplete = true;
    }

    async Task LoadLevel(int level)
    {
        // TO DO: Load level data from DataManager and initialize level
    }

    void Update()
    {
        if (!_isLoadingComplete) return;
        if (_isPaused || _isGameOver) return;

        //TO DO: Call every child entity update
    }
}
