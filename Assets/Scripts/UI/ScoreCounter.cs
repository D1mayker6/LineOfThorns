using System;
using Player;
using UnityEngine;

namespace UI
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;

        private int _score;

        private float _time;

        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                OnScoreChanged?.Invoke();
            }
        } 
        
        public event Action OnScoreChanged;


        private void Update()
        {
            _time += Time.deltaTime;
            if (_time >= 0.1f)
            {
                Score++;
                _time = 0;
            }
            Debug.Log(Score);
        }
    }
}
