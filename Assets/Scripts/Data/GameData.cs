using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
    public class GameData : ScriptableObject
    {
        public int Coins;
    
        public int Record;
    
        public List<bool> OpenedColors = new List<bool>(8);
    }
}
