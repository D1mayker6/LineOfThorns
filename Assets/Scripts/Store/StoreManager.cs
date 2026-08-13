using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Store
{
    public class StoreManager : MonoBehaviour
    {
        [SerializeField] private List<StoreColor> _colors;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _forMoneyButton;
        [SerializeField] private Button _forADButton;
        [SerializeField] private StoreColor _currentColor;
        [SerializeField] private DataManager _dataManager;
        [SerializeField] private GameData _gameData;
        [SerializeField] private TextMeshProUGUI _moneyCountText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private int _currentColorPrice;
        
        [SerializeField] private SceneLoader _sceneLoaderPrefab;
        
        private void Awake()
        {
            _backButton.onClick.AddListener(Back);
            _forMoneyButton.onClick.AddListener(ForMoney);
            _forADButton.onClick.AddListener(ForAD);
        }

        private void Start()
        {
            RefreshUIMoney();
            for (var i = 0; i < _colors.Count; i++)
                _colors[i].IsOpen = _gameData.OpenedColors[i];
        }


        private void Back()
        {
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(0);
        }

        private void ForMoney()
        {
            if (_gameData.Coins >= _currentColorPrice)
            {
                _gameData.Coins -= _currentColorPrice;
                RandomizeColor();
                RefreshUIMoney();
            }
        }
        
        private void RefreshUIMoney() => _moneyCountText.text = _gameData.Coins.ToString();

        private void ForAD()
        { 
            RandomizeColor(); 
        }

        private void RandomizeColor()
        {
            var count = _colors.Count;
            var random = new Random();
            while (true)
            {
                var color = random.Next(0, count);
                if (_gameData.OpenedColors[color])
                    continue;
                _colors[color].IsOpen = true;
                _gameData.OpenedColors[color] = true;
                _currentColor =  _colors[color];
                return;
            }
                
        }

        private void OnDestroy()
        {
            _dataManager.SaveGameData();
            Debug.Log("GameData saved");
        }
    }
}
