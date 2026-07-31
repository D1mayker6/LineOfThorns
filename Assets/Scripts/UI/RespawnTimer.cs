using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RespawnTimer : MonoBehaviour
    {

        [SerializeField] private Image _timer;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Button _button;
        [SerializeField] private Animator _animator;

        
        public event Action OnTimerEnded;

        private int _timerValue;

        private void Start()
        {
            _timerValue = 3;
            _animator.SetFloat("timer", _timerValue);
        }

        private void Update()
        {
            _timerText.text = _timerValue.ToString();
            if (_timerValue < 0)
            { 
               OnTimerEnded?.Invoke();
               Destroy(gameObject); 
            }
        }

        public void MinusSecond()
        {
            _timerValue--;
        }
        
        
    }
}
