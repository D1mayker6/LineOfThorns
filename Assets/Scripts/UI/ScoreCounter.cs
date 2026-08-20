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

        [SerializeField] private  int _diffChangeValue;

        private bool _isStopped;
        
        [SerializeField] private int _roomValue = 50;

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

        public event Action<int> OnDiffReached;
        
        public event Action OnCounterSwitch;



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
            var diffLvl = Score / _diffChangeValue;
            Debug.Log(diffLvl);
            if (diffLvl > _currentDiff)
            {
                _currentDiff = diffLvl;
                OnDiffReached?.Invoke(_currentDiff);    
            }
            
        }

        public void SwitchCounter()
        {
            _isStopped = !_isStopped;
            OnCounterSwitch?.Invoke();
        }

        public void AddScore()
        {
            Score += _roomValue;
        }
    }
}
