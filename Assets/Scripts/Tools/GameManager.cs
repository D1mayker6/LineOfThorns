using System;
using Player;
using UI;
using UnityEngine;

namespace Tools
{
    public class GameManager : MonoBehaviour
    {
    
        [SerializeField] private PlayerMovement  _playerMovement;
        [SerializeField] private GameObject _retryCanvasPrefab;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private GameObject _respawnTimerPrefab;
        private ScoreCounter _scoreCounter;

        void Start()
        {
            _playerMovement.OnPlayerDied += RespawnTimer;
        }

        private void RespawnTimer()
        {
            var timer = Instantiate(_respawnTimerPrefab.gameObject);
            timer.GetComponent<RespawnTimer>().OnTimerEnded += RestartGame;
        }

        void RestartGame()
        {
            Debug.Log("Restarting game");   
            var retryCanvas = Instantiate(_retryCanvasPrefab);
            _scoreCounter = _scoreView.GetComponentInChildren<ScoreCounter>();
            var score = _scoreCounter.Score;
            retryCanvas.GetComponent<RetryCanvas>().Initialize(score);
            _scoreView.StopCounterView();

        }

    }
}
