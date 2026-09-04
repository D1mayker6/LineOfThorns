using System;
using Audio;
using Data;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace UI
{
    public class RetryCanvas : MonoBehaviour
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _recordText;
        [SerializeField] private SceneLoader _sceneLoaderPrefab;
        [SerializeField] private GameObject _recordPanel;
        [SerializeField] private TextMeshProUGUI _earnText;
        [SerializeField] private CoinsConverter _coinsConverter;
        [SerializeField] private GameData _gameData;
        [SerializeField] private DataManager _dataManager;
        [SerializeField] private SimpleYandexAds _yandexAds;
        
        private AudioManager _audioManager;

        private int _score;
        private int _record;
        public void Initialize(int score, AudioManager audioManager)
        {
            _score =  score;
            _audioManager = audioManager;
            _scoreText.text = $"Score: {_score}";
            if (_gameData.Record < _score)
                SetNewRecord(_score);
        }

        private void SetNewRecord(int record)
        {
            _record = record;
            _gameData.Record = record;
            _recordPanel.SetActive(true);
        }
        
        private void Start()
        {
            var coins = _coinsConverter.ConvertScore(_score);
            _gameData.Coins += coins;
            _earnText.text = $"Earned: {coins}";
            _record = _gameData.Record;
            _recordText.text = $"Record: {_record}";
        }

        private void OnEnable()
        {
            _mainMenuButton.onClick.AddListener(Menu);
            _retryButton.onClick.AddListener(Retry);
        }

        private void Menu()
        {
            if (_gameData.AttempsCount % 5 == 0)
            {
                _yandexAds.ShowInterstitial();   
                Debug.Log("Yandex Ads Show Interstitial");
            }
            _gameData.AttempsCount++;
            if(_audioManager != null)
                StartCoroutine(_audioManager.UpperPitch());
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(1);
        }

        private void Retry()
        {
            if (_gameData.AttempsCount % 5 == 0)
            {
                _yandexAds.ShowInterstitial();   
                Debug.Log("Yandex Ads Show Interstitial");
            }
            _gameData.AttempsCount++;
            if(_audioManager != null)
                StartCoroutine(_audioManager.UpperPitch());
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(2);
        }

        private void OnDisable()
        {
            _mainMenuButton.onClick.RemoveListener(Menu);
            _retryButton.onClick.RemoveListener(Retry);
            _dataManager.SaveGameData();
        }
    }
}
