using System;
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
        [SerializeField] private Button _aboutButton;
        [SerializeField] private Button _exitButton;
       
        
        [SerializeField] private GameObject _cinematicTexture;
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private VideoClip _menuClip;

        [SerializeField] private GameObject _loadingScreen;
        
        private VideoPlayer _videoPlayer;

        private void Start()
        {
            _mainMenuPanel.SetActive(false);
            _videoPlayer = _cinematicTexture.GetComponentInChildren<VideoPlayer>();
            _videoPlayer.loopPointReached += VideoPlayerOnloopPointReached;
            _videoPlayer.Play();
            
            _playButton.onClick.AddListener(Play);
            _storeButton.onClick.AddListener(Store);
            _settingsButton.onClick.AddListener(Settings);
            _aboutButton.onClick.AddListener(About);
            _exitButton.onClick.AddListener(Exit);
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
            var loader = Instantiate(_loadingScreen);
            loader.GetComponent<SceneLoader>().LoadNewScene(1);
        }
        
        private void Exit()
        {
            Application.Quit();
        }

        private void About()
        {
            throw new NotImplementedException();
        }

        private void Settings()
        {
            throw new NotImplementedException();
        }

        private void Store()
        {
            throw new NotImplementedException();
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(Play);
        }
    }
}
