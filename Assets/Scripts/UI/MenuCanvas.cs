using System;
using Audio;
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
        
        [SerializeField] private VideoPlayer _videoPlayer;

        [SerializeField] private AudioClip _clickSound;
        private void Start()
        {
            _mainMenuPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
        }

        private void OnEnable()
        {
            
            _playButton.onClick.AddListener(Play);
            _storeButton.onClick.AddListener(Store);
            _settingsButton.onClick.AddListener(Settings);
            _exitButton.onClick.AddListener(Exit);
            
            _telegramButton.onClick.AddListener(GoToTelegram);
            _youtubeButton.onClick.AddListener(GoToYoutube);
        }

        private void Play()
        {
            AudioSource.PlayClipAtPoint(_clickSound, transform.position);
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(2);
        }
        
        private void Exit()
        {
            Application.Quit();
        }

        private void Settings()
        {
            AudioSource.PlayClipAtPoint(_clickSound, transform.position);
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(4);
        }

        private void Store()
        {
            AudioSource.PlayClipAtPoint(_clickSound, transform.position);
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(3);
        }


        private void GoToTelegram()
        {
            AudioSource.PlayClipAtPoint(_clickSound, transform.position);
            var redirect = Instantiate(_redirectCanvasPrefab);
            redirect.Initialize(SocialNetwork.Telegram, "https://t.me/d1mayker6WS");
        }

        private void GoToYoutube()
        {
            AudioSource.PlayClipAtPoint(_clickSound, transform.position);
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
