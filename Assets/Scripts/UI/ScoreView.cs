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
            UpdateScoreView();
        }

        private void UpdateScoreView()
        {
            _text.text = _counter.Score.ToString();
        }

        public void StopCounterView()
        {
            _text.text = "";
            _counter.StopCounter();
        }

        private void ResumeCounterView()
        {
            _text.text = _counter.Score.ToString();
            _counter.ResumeCounter();
        }
        
        
    }
    
}
