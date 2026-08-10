using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Store
{
    public class StoreManager : MonoBehaviour
    {
        [SerializeField] private List<StoreColor> _colors;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _forMoneyButton;
        [SerializeField] private Button _forADButton;
        [SerializeField] private DataManager _dataManager;
        [SerializeField] private GameData _gameData;
        [SerializeField] private TextMeshProUGUI _moneyCountText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private int _currentColorPrice;
        
        private void Awake()
        {
            _backButton.onClick.AddListener(Back);
            _forMoneyButton.onClick.AddListener(ForMoney);
            _forADButton.onClick.AddListener(ForAD);
        }

        private void Start()
        {
            _moneyCountText.text = _gameData.Coins.ToString();
            for (var i = 0; i < _colors.Count; i++)
                _colors[i].IsOpen = _gameData.OpenedColors[i];
        }


        private void Back()
        {
            Destroy(gameObject);
        }

        private void ForMoney()
        {
            if (_gameData.Coins >= _currentColorPrice)
            {
                _gameData.Coins -= _currentColorPrice;
                StartCoroutine(RandomizeColor()); 
            }
        }

        private void ForAD()
        { 
            StartCoroutine(RandomizeColor()); 
        }

        private IEnumerator RandomizeColor()
        {
            return null;
        }
    }
}
