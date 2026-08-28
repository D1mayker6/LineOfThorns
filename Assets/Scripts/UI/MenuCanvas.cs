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
        [SerializeField] private VideoClip _menuClip;

        [SerializeField] private SceneLoader _sceneLoaderPrefab;

        [SerializeField] private RedirectCanvas _redirectCanvasPrefab;
        
        [SerializeField] private DataManager _dataManager;
        
        private VideoPlayer _videoPlayer;
        
        [SerializeField] private AudioChannel _audioChannel;

        private void Start()
        {
            Application.targetFrameRate = 60;
            _mainMenuPanel.SetActive(false);
            _videoPlayer = _cinematicTexture.GetComponentInChildren<VideoPlayer>();
            _videoPlayer.loopPointReached += VideoPlayerOnloopPointReached;
            _videoPlayer.Play();
            
            _playButton.onClick.AddListener(Play);
            _storeButton.onClick.AddListener(Store);
            _settingsButton.onClick.AddListener(Settings);
            _exitButton.onClick.AddListener(Exit);
            
            _telegramButton.onClick.AddListener(GoToTelegram);
            _youtubeButton.onClick.AddListener(GoToYoutube);
            
            _dataManager.LoadData();
            
            _audioChannel.Play();
        }

        private void OnEnable()
        {
            //_videoPlayer.loopPointReached -= VideoPlayerOnloopPointReached;
            
            _playButton.onClick.RemoveListener(Play);
            _storeButton.onClick.RemoveListener(Store);
            _settingsButton.onClick.RemoveListener(Settings);
            _exitButton.onClick.RemoveListener(Exit);
            
            _telegramButton.onClick.RemoveListener(GoToTelegram);
            _youtubeButton.onClick.RemoveListener(GoToYoutube);
        }


        private void VideoPlayerOnloopPointReached(VideoPlayer source)
        {
            _videoPlayer.clip = _menuClip;
            _videoPlayer.isLooping = true;
            _videoPlayer.Play();
            _mainMenuPanel.SetActive(true);
        }

        private void Play()
        {
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(1);
        }
        
        private void Exit()
        {
            Application.Quit();
        }

        private void Settings()
        {
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(3);
        }

        private void Store()
        {
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(2);
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
