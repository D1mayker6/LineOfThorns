using UnityEngine;

namespace Tools
{
    public class CoinsConverter: MonoBehaviour
    {
        [SerializeField] private int _coinsMultiplier;



        private int ConvertScore(int score)
        {
            var coins = score / 10;
            return coins;
        }
    }
}