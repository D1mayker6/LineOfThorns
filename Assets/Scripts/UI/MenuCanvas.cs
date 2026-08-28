using System;
using Data;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace UI
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _storeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;
        
        [SerializeField] private Button _telegramButton;
        [SerializeField] private Button _youtubeButton;
       
        
        [SerializeField] private GameObject _cinematicTexture;
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private SceneLoader _sceneLoaderPrefab;

        [SerializeField] private RedirectCanvas _redirectCanvasPrefab;
        
        [SerializeField] private DataManager _dataManager;
        
        private VideoPlayer _videoPlayer;
        
        [SerializeField] private AudioChannel _audioChannel;

        private void Start()
        {
            
            _mainMenuPanel.SetActive(false);
            _videoPlayer = _cinematicTexture.GetComponentInChildren<VideoPlayer>();
            _audioChannel.Play();
            _mainMenuPanel.SetActive(true);
        }

        private void OnEnable()
        {
            //_videoPlayer.loopPointReached -= VideoPlayerOnloopPointReached;
            
            _playButton.onClick.AddListener(Play);
            _storeButton.onClick.AddListener(Store);
            _settingsButton.onClick.AddListener(Settings);
            _exitButton.onClick.AddListener(Exit);
            
            _telegramButton.onClick.AddListener(GoToTelegram);
            _youtubeButton.onClick.AddListener(GoToYoutube);
        }

        private void Play()
        {
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(2);
        }
        
        private void Exit()
        {
            Application.Quit();
        }

        private void Settings()
        {
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(4);
        }

        private void Store()
        {
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(3);
        }


        private void GoToTelegram()
        {
            var redirect = Instantiate(_redirectCanvasPrefab);
            redirect.Initialize(SocialNetwork.Telegram, "https://t.me/d1mayker6WS");
        }

        private void GoToYoutube()
        {
            var redirect = Instantiate(_redirectCanvasPrefab);
            redirect.Initialize(SocialNetwork.Youtube, "https://www.youtube.com/channel/UCiMu-22dEI8MgAry_YOUa5w");
        }
        

        private void OnDestroy()
        {
            _dataManager.SaveGameData();
            _dataManager.SaveSettingsData();
            _playButton.onClick.RemoveListener(Play);
        }
    }
}
