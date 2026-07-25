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
        public void Initialize(int score)
        {
            gameObject.SetActive(true);
            _scoreText.text = "Score: " + score;
        }
        
        private void Start()
        {
            _mainMenuButton.onClick.AddListener(Menu);
            _retryButton.onClick.AddListener(Retry);
        }

        private void Menu()
        {
            SceneManager.LoadScene("Menu");
        }

        private void Retry()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
