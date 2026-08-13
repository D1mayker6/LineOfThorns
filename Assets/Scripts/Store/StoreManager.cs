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
        
        [SerializeField] private SceneLoader _sceneLoaderPrefab;
        
        private void Awake()
        {
            _backButton.onClick.AddListener(Back);
            _forMoneyButton.onClick.AddListener(ForMoney);
            _forADButton.onClick.AddListener(ForAD);
            foreach (var color in  _colors)
                color.OnColorChosen += SetCurrentColor;
            
        }

        private void SetCurrentColor(Color backgroundColor, Color blockColor)
        {
            _gameData.BackgroundColor = ColorUtility.ToHtmlStringRGBA(backgroundColor);
            _gameData.BlockColor = ColorUtility.ToHtmlStringRGBA(blockColor);
            foreach (var color in _colorsImages)
                color.sprite = _defaultSprite;
        }

        private void Start()
        {
            RefreshUIMoney();
            CheckMoney();
            CheckMaxCountSkins();
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
            var count = _colors.Count;
            var random = new Random();
            while (true)
            {
                var color = random.Next(0, count);
                if (_gameData.OpenedColors[color])
                    continue;
                _colors[color].IsOpen = true;
                _gameData.OpenedColors[color] = true;
                //_currentColor =  _colors[color];
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
