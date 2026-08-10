using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private SettingsData _settingsData;

        private string GetPathGameData() => Path.Combine(Application.persistentDataPath, "GameData");
        
        private string GetPathSettingsData() => Path.Combine(Application.persistentDataPath, "SettingsData");
        
        public void LoadData()
        {
           var gamepath =  GetPathGameData();
           if (File.Exists(gamepath))
           {
                var json = File.ReadAllText(gamepath);
                JsonConvert.PopulateObject(json, _gameData);
           }
           
           var settingspath = GetPathSettingsData();
           if (File.Exists(settingspath))
           {
               var json = File.ReadAllText(settingspath);
               JsonConvert.PopulateObject(json, _settingsData);
           }
           
        }

        public void SaveData()
        {
            
        }
    }
}
