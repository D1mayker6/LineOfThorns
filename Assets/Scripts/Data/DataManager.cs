using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private SettingsData _settingsData;

        private string _gamePath;
        private string _settingsPath;

        private void Awake()
        {
            _gamePath = Path.Combine(Application.persistentDataPath, "GameData.json");
            _settingsPath = Path.Combine(Application.persistentDataPath, "SettingsData.json");
        }
        
        public void LoadData()
        {
           if (File.Exists(_gamePath))
           {
                var json = File.ReadAllText(_gamePath);
                JsonConvert.PopulateObject(json, _gameData);
           }
           
           if (File.Exists(_settingsPath))
           {
               var json = File.ReadAllText(_settingsPath);
               JsonConvert.PopulateObject(json, _settingsData);
           }
           
        }

        public void SaveGameData() => File.WriteAllText(_gamePath, JsonConvert.SerializeObject(_gameData));
        
        public void SaveSettingsData() => File.WriteAllText(_settingsPath, JsonConvert.SerializeObject(_settingsData));

        public void DeleteGameData()
        {
            File.Delete(_gamePath);
            _gameData.ResetData();
            SaveGameData();
            Application.Quit();
        }
            
        
    }
}
