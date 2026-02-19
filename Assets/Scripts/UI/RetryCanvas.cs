using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class RetryCanvas : MonoBehaviour
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Text _scoreText;

        private void Start()
        {
            _mainMenuButton.onClick.AddListener(Menu);
            _mainMenuButton.onClick.AddListener(Retry);
        }

        private void Menu()
        {
            SceneManager.LoadScene("Menu");
        }

        private void Retry()
        {
            
        }
    }
}
