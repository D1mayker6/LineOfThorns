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
    }
}
