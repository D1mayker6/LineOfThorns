using System;
using System.Collections;
using Player;
using UI;
using UnityEngine;

namespace Tools
{
    public class GameManager : MonoBehaviour
    {
    
        [SerializeField] private PlayerMovement  _playerMovement;
        [SerializeField] private RetryCanvas _retryCanvasPrefab;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private RespawnTimer _respawnTimerPrefab;
        [SerializeField] private ScoreCounter _scoreCounter;

        void Start()
        {
            _playerMovement.OnPlayerDied += TryRespawnTimer;
            _scoreCounter = _scoreView.GetComponentInChildren<ScoreCounter>();

        }

        private void TryRespawnTimer()
        {
            _scoreView.StopCounterView();
            if(_scoreCounter.Score > 500)
                StartCoroutine(RespawnTimerCoroutine());
            else
                RestartGame();
        }

        IEnumerator RespawnTimerCoroutine()
        {
            yield return new WaitForSeconds(2);
            var timer = Instantiate(_respawnTimerPrefab);
            timer.OnTimerEnded += RestartGame;
        }

        void RestartGame()
        {
            var retryCanvas = Instantiate(_retryCanvasPrefab);
            var score = _scoreCounter.Score;
            retryCanvas.Initialize(score);
        }

    }
}
