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
        [SerializeField] private SceneLoader _sceneLoaderPrefab;
        [SerializeField] private GameObject _recordPanel;
        [SerializeField] private TextMeshProUGUI _earnText;
        [SerializeField] private CoinsConverter _coinsConverter;

        private int _score;
        public void Initialize(int score)
        {
            _score =  score;
            _scoreText.text = $"Score: {_score}";
        }
        
        private void Start()
        {
            _mainMenuButton.onClick.AddListener(Menu);
            _retryButton.onClick.AddListener(Retry);
            _earnText.text = $"Earned: {_coinsConverter.ConvertScore(_score)}";
        }

        private void Menu()
        {
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(0);
        }

        private void Retry()
        {
            var loader = Instantiate(_sceneLoaderPrefab);
            loader.LoadNewScene(1);
        }
    }
}
