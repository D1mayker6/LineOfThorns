using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Player;
using TMPro;
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
        
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Color _blockColor;
        [SerializeField] private Color _uiColor;

        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private TextMeshProUGUI _scoreViewText;
        [SerializeField] private GameData _gameData;
        
        private bool _timerFirstTime = false;
        
        private List<SpriteRenderer> _spriteRenderers = new List<SpriteRenderer>(64);


        void Awake()
        {
            _playerMovement.OnPlayerDied += DeathPlayer;
            _scoreCounter.OnDiffReached += ShowDiffView;
            InitializeColors();
        }

        private void Start()
        {
            if (Camera.main != null) 
                Camera.main.backgroundColor = _backgroundColor;
            _scoreViewText.color = _uiColor;
        }

        private void InitializeColors()
        {
            ColorUtility.TryParseHtmlString(_gameData.BackgroundColor, out _backgroundColor);
            ColorUtility.TryParseHtmlString(_gameData.BlockColor, out _blockColor);
            ColorUtility.TryParseHtmlString(_gameData.UIColor, out _uiColor);
        }
        
        public void RecolorRoom(GameObject room)
        {
            _spriteRenderers.Clear();
            _spriteRenderers.AddRange(room.GetComponentsInChildren<SpriteRenderer>());
            foreach (var spriteRenderer in _spriteRenderers)
                spriteRenderer.color = _blockColor;
            
        }

        private void DeathPlayer() => StartCoroutine(DeathPlayerCoroutine());

        private void ShowDiffView(int diff = 0) => Instantiate(_diffViewPrefab);

        private IEnumerator DeathPlayerCoroutine()
        {
            Debug.Log("DeathPlayerCoroutine");
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
            {
                Debug.Log("Вызван из трай респавн");
                RestartGame();
            }
        }

        private void RespawnTimerCoroutine()
        {
            _respawnTimerPrefab = Instantiate(_respawnTimerPrefab);
            _respawnTimerPrefab.OnTimerEnded += RestartGame;
            _respawnTimerPrefab.OnExtraLive += ExecuteExtraLive;
        }
        
        

        private void ExecuteExtraLive()
        {
            _playerMovement.gameObject.SetActive(true);
            _scoreCounter.SwitchCounter();
        }

        void RestartGame()
        {
            var retryCanvas = Instantiate(_retryCanvasPrefab);
            var score = _scoreCounter.Score;
            retryCanvas.Initialize(score);
        }

        private void OnDisable()
        {
            _playerMovement.OnPlayerDied -= DeathPlayer;
            _scoreCounter.OnDiffReached -= ShowDiffView;
            _respawnTimerPrefab.OnTimerEnded -= RestartGame;
            _respawnTimerPrefab.OnExtraLive -= ExecuteExtraLive;
        }
    }
}
