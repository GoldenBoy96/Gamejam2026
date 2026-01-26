using System;
using OurUtils;
using UnityEngine;

namespace Gamejam2026
{
    public class DataManager 
    {
        public PlayerInventory PlayerInventoryData { get; private set; }
        public PlayerProgression PlayerProgressionData { get; private set; }
        public void LoadPlayerData()
        {
            PlayerInventoryData = JsonHelper.ReadData<PlayerInventory>(Application.persistentDataPath + "/PlayerInventory");
            PlayerProgressionData = JsonHelper.ReadData<PlayerProgression>(Application.persistentDataPath + "/PlayerProgression");
        }

        public void SavePlayerData()
        {
            JsonHelper.SaveData(PlayerInventoryData, Application.persistentDataPath + "/PlayerInventory");
            JsonHelper.SaveData(PlayerProgressionData, Application.persistentDataPath + "/PlayerProgression");
        }

        public void ResetPlayerData()
        {
            PlayerInventoryData = new PlayerInventory();
            PlayerProgressionData = new PlayerProgression();
        }
    }

    [Serializable]
    public class PlayerInventory
    {
        [SerializeField] private bool _isOwnSkill_1 = false;
        [SerializeField] private bool _isOwnSkill_2 = false;
        public bool IsOwnSkill_1 { get => _isOwnSkill_1; set => _isOwnSkill_1 = value; }
        public bool IsOwnSkill_2 { get => _isOwnSkill_2; set => _isOwnSkill_2 = value; }
    }


    [Serializable]
    public class PlayerProgression
    {
        [SerializeField] private int _currentLevel = 1;
        public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
    }
}
