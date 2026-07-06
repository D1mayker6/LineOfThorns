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

        private bool _isStopped;

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
            if (!_isStopped)
            {
                _time += Time.deltaTime;
                if (_time >= 0.1f)
                {
                    Score++;
                    _time = 0;
                }
            }
        }

        public void StopCounter()
        {
            _isStopped = true;
        }

        public void ResumeCounter()
        {
            _isStopped = false;
        }

        public void AddScore(int value)
        {
            Score += value;
        }
    }
}
