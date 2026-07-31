using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class RetryCanvas : MonoBehaviour
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject _sceneLoaderPrefab;
        public void Initialize(int score)
        {
            _scoreText.text = "Score: " + score;
        }
        
        private void Start()
        {
            _mainMenuButton.onClick.AddListener(Menu);
            _retryButton.onClick.AddListener(Retry);
        }

        private void Menu()
        {
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.GetComponent<SceneLoader>().LoadNewScene(0);
        }

        private void Retry()
        {
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.GetComponent<SceneLoader>().LoadNewScene(1);
        }
    }
}
