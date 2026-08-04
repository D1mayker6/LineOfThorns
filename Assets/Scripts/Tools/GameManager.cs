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
        [SerializeField] private GameObject _deathPlayerParticlePrefab;
        [SerializeField] private GameObject _diffViewPrefab;

        [SerializeField] private ScoreCounter _scoreCounter;

        void Start()
        {
            _playerMovement.OnPlayerDied += DeathPlayer;
            _scoreCounter.OnDiffReached += ShowDiffView;
        }

        private void DeathPlayer() => StartCoroutine(DeathPlayerCoroutine());

        private void ShowDiffView() => Instantiate(_diffViewPrefab);

        private IEnumerator DeathPlayerCoroutine()
        {
            _playerMovement.gameObject.SetActive(false);
            Instantiate(_deathPlayerParticlePrefab, _playerMovement.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2);
            TryRespawnTimer();
        }

        private void TryRespawnTimer()
        {
            _scoreView.StopCounterView();
            if(_scoreCounter.Score > 500)
                RespawnTimerCoroutine();
            else
                RestartGame();
        }

        private void RespawnTimerCoroutine()
        {
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
