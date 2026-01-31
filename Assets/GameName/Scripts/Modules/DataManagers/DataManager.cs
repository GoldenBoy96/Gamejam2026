using System;
using OurUtils;
using UnityEngine;

namespace Gamejam2026
{
    public class DataManager 
    {
        public PlayerInventory PlayerInventoryData { get; private set; }
        public PlayerProgression PlayerProgressionData { get; private set; }
        public VNSaveData VNSaveData { get; private set; }

        public void LoadPlayerData()
        {
            PlayerInventoryData = JsonHelper.ReadData<PlayerInventory>(Application.persistentDataPath + "/PlayerInventory");
            PlayerProgressionData = JsonHelper.ReadData<PlayerProgression>(Application.persistentDataPath + "/PlayerProgression");
            VNSaveData = JsonHelper.ReadData<VNSaveData>(Application.persistentDataPath + "/VNSaveData");
            if (VNSaveData == null) VNSaveData = new VNSaveData();
        }

        public void SavePlayerData()
        {
            JsonHelper.SaveData(PlayerInventoryData, Application.persistentDataPath + "/PlayerInventory");
            JsonHelper.SaveData(PlayerProgressionData, Application.persistentDataPath + "/PlayerProgression");
            JsonHelper.SaveData(VNSaveData, Application.persistentDataPath + "/VNSaveData");
            JsonHelper.SaveData(VNSaveData, Application.persistentDataPath + "/VNSaveData");
        }

        public void ResetPlayerData()
        {
            PlayerInventoryData = new PlayerInventory();
            PlayerProgressionData = new PlayerProgression();
        }
        public void SaveVNProgress(string levelId, string nodeId, int score)
        {
            if (VNSaveData == null) VNSaveData = new VNSaveData();
            VNSaveData.levelId = levelId;
            VNSaveData.currentNodeId = nodeId;
            VNSaveData.currentScore = score;

            JsonHelper.SaveData(VNSaveData, Application.persistentDataPath + "/VNSaveData");
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
