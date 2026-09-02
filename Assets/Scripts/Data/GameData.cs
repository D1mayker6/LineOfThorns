using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
    public class GameData : ScriptableObject
    {
        public int Coins;
    
        public int Record;
    
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public List<bool> OpenedColors;

        public string BackgroundColor = "#000000";

        public string BlockColor = "#FFFFFF";
        
        public string UIColor = "#FFFFFF";

        public int CurentColor;
        
        private readonly int _colorsCount = 8;
        
        public int AttempsCount = 0;
        
        public void ResetData()
        {
            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(CreateInstance<GameData>()), this);
            for (var i = 0; i < _colorsCount; i++)
            {
                OpenedColors.Add(false);
            }

            OpenedColors[0] = true;
        }
    }
}
