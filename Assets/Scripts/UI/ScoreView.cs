using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        private ScoreCounter _counter;
        [SerializeField] private Text _text;

        private void Start()
        {
            _counter = GetComponentInChildren<ScoreCounter>();
            _counter.OnScoreChanged += UpdateScoreView;
            UpdateScoreView();
        }

        private void UpdateScoreView()
        {
            _text.text = _counter.Score.ToString();
        }
        
        
    }
    
}
