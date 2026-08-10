using UnityEngine;

namespace Tools
{
    public class CoinsConverter: MonoBehaviour
    {
        [SerializeField] private int _coinsMultiplier;



        public int ConvertScore(int score)
        {
            var coins = score / _coinsMultiplier;
            return coins;
        }
    }
}