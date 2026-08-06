using System;
using TMPro;
using UnityEngine;
namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private ScoreCounter _counter;
        [SerializeField] private TextMeshProUGUI _text;


        private void Start()
        {
            _counter.OnScoreChanged += UpdateScoreView;
            _counter.OnCounterSwitch += SwitchCounterView;
            UpdateScoreView();
        }

        private void UpdateScoreView()
        {
            _text.text = _counter.Score.ToString();
        }

        private void SwitchCounterView()
        {
            _text.enabled = !_text.enabled;
        }
        
        
    }
    
}
