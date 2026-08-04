using System;
using Player;
using UnityEngine;

namespace UI
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private int _score;

        private float _time;

        private int _currentDiff = 0;

        private bool _isStopped;
        
        private int _roomValue = 100;

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

        public event Action OnDiffReached;


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
            Debug.Log(_currentDiff);
            var diffLvl = Score / 1000;

            if (diffLvl > _currentDiff)
            {
                OnDiffReached?.Invoke();    
                _currentDiff++;
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

        public void AddScore()
        {
            Score += _roomValue;
        }
    }
}
