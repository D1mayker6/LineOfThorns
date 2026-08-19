using System;
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
        [SerializeField] private List<Image> _colorsImages;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _forMoneyButton;
        [SerializeField] private Button _forADButton;
        [SerializeField] private DataManager _dataManager;
        [SerializeField] private GameData _gameData;
        [SerializeField] private TextMeshProUGUI _moneyCountText;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private int _currentColorPrice;
        [SerializeField] private Sprite _defaultSprite; 
        [SerializeField] private Sprite _selectedSprite;
        
        [SerializeField] private SceneLoader _sceneLoaderPrefab;
        

        private void OnEnable()
        {
            _backButton.onClick.AddListener(Back);
            _forMoneyButton.onClick.AddListener(ForMoney);
            _forADButton.onClick.AddListener(ForAD);
            foreach (var color in  _colors)
                color.OnColorChosen += SetCurrentColor;
        }


        private void Start()
        {
            RefreshUIMoney();
            CheckMoney();
            CheckMaxCountSkins();
            for (var i = 0; i < _colors.Count; i++)
                _colors[i].IsOpen = _gameData.OpenedColors[i];
            ChangeSelectedSprite();
        }


        private void Back()
        {
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(0);
        }
        private void SetCurrentColor(Color backgroundColor, Color blockColor, Color uiColor, int id)
        {
            _gameData.BackgroundColor =$"#{ColorUtility.ToHtmlStringRGB(backgroundColor)}";
            _gameData.BlockColor = $"#{ColorUtility.ToHtmlStringRGB(blockColor)}";
            _gameData.UIColor = $"#{ColorUtility.ToHtmlStringRGB(uiColor)}";
            foreach (var color in _colorsImages)
                color.sprite = _defaultSprite;
            _gameData.CurentColor = id;
            ChangeSelectedSprite();
        }

        private void ChangeSelectedSprite() => _colorsImages[_gameData.CurentColor].sprite = _selectedSprite;

        
        private void ForMoney()
        {
                _gameData.Coins -= _currentColorPrice;
                RandomizeColor();
                RefreshUIMoney();
                CheckMaxCountSkins();
        }
        
        private void RefreshUIMoney() => _moneyCountText.text = _gameData.Coins.ToString();

        private void CheckMaxCountSkins()
        {
            foreach (var color in _gameData.OpenedColors)
                if (!color)
                    return;
            _forADButton.interactable = false;
            _forMoneyButton.interactable = false;
            
        }

        private void CheckMoney()
        {
            if (_gameData.Coins <= _currentColorPrice)
            {
                _forMoneyButton.interactable = false;
                return;
            }
            _forMoneyButton.interactable = true;
        }
        

        private void ForAD()
        { 
            RandomizeColor(); 
        }

        private void RandomizeColor()
        {
            while (true)
            {
                var color = UnityEngine.Random.Range(0, _colors.Count);
                if (_gameData.OpenedColors[color])
                    continue;
                _colors[color].IsOpen = true;
                _gameData.OpenedColors[color] = true;
                return;
            }
                
        }

        private void OnDisable()
        {
            _dataManager.SaveGameData();
            _backButton.onClick.RemoveListener(Back);
            _forMoneyButton.onClick.RemoveListener(ForMoney);
            _forADButton.onClick.RemoveListener(ForAD);
            foreach (var color in  _colors)
                color.OnColorChosen -= SetCurrentColor;
        }
    }
}
