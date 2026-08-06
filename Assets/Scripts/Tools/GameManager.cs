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
        [SerializeField] private RespawnTimer _respawnTimerPrefab;
        [SerializeField] private GameObject _deathPlayerParticlePrefab;
        [SerializeField] private GameObject _diffViewPrefab;

        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private Transform _spawnpoint;
        
        private bool _timerFirstTime = false;

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
            _scoreCounter.SwitchCounter();
            Instantiate(_deathPlayerParticlePrefab, _playerMovement.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2);
            TryRespawnTimer();
        }

        private void TryRespawnTimer()
        {
            if (_scoreCounter.Score > 500 && !_timerFirstTime)
            {
                RespawnTimerCoroutine();
                _timerFirstTime = true;
            }
            else
                RestartGame();
        }

        private void RespawnTimerCoroutine()
        {
            var timer = Instantiate(_respawnTimerPrefab);
            timer.OnTimerEnded += RestartGame;
            timer.OnExtraLive += ExecuteExtraLive;
        }

        private void ExecuteExtraLive()
        {
            _playerMovement.gameObject.SetActive(true);
            _playerMovement.transform.position = _spawnpoint.position;
            var rb = _playerMovement.GetComponent<Rigidbody2D>();
            rb.gravityScale = Mathf.Abs(rb.gravityScale);
            _playerMovement.AbsForcePower();
            _scoreCounter.SwitchCounter();
        }

        void RestartGame()
        {
            var retryCanvas = Instantiate(_retryCanvasPrefab);
            var score = _scoreCounter.Score;
            retryCanvas.Initialize(score);
        }

    }
}
