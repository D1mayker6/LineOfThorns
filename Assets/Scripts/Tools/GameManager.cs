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
            _playerMovement.OnPlayerDied += RespawnTimer;
            _scoreCounter = _scoreView.GetComponentInChildren<ScoreCounter>();

        }

        private void RespawnTimer()
        {
            StartCoroutine(RespawnTimerCoroutine());
        }

        IEnumerator RespawnTimerCoroutine()
        {
            _scoreView.StopCounterView();
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
