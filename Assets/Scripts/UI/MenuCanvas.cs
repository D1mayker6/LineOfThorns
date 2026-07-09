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
        [SerializeField] private GameObject _cinematicTexture;
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private VideoClip _menuClip;
        
        private VideoPlayer _videoPlayer;

        private void Start()
        {
            _mainMenuPanel.SetActive(false);
            _videoPlayer = _cinematicTexture.GetComponentInChildren<VideoPlayer>();
            _videoPlayer.loopPointReached += VideoPlayerOnloopPointReached;
            _playButton.onClick.AddListener(Play);
            _videoPlayer.Play();
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
            SceneManager.LoadScene("Game");
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(Play);
        }
    }
}
