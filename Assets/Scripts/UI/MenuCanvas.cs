using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        private void Start()
        {
            _playButton.onClick.AddListener(Play);
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
