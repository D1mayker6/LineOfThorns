using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AttemptsView : MonoBehaviour
    {   
        private Text _attemptsText;
        private AttemptsCounter _attemptsCounter;

        private void Start()
        {
            _attemptsText = GetComponent<Text>();
            _attemptsCounter = GetComponentInChildren<AttemptsCounter>();
        }

        private void OnEnable()
        {
            _attemptsCounter.OnValueChanged += UpdateView;
        }

        private void OnDisable()
        {
            _attemptsCounter.OnValueChanged -= UpdateView;
        }

        private void UpdateView()
        {
            _attemptsText.text = $"Попытка {_attemptsCounter.Attempts}";
        }
    }
}
