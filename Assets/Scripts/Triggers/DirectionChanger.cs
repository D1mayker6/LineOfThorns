using System;
using Data;
using Enums;
using Player;
using UnityEngine;

namespace Triggers
{
    public class DirectionChanger : MonoBehaviour
    {
        [SerializeField] private Direction _direction;
        [SerializeField] private int _touchCount = 1;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] private SettingsData _settings;
        
        private PlayerMovement _playerMovement;
        private Vector3 _localScale;
        
        public int TouchCount
        {
            get => _touchCount;
            set
            {
                _touchCount = value;
                if (_touchCount == 0)
                    DestroyTrigger();
            }
        }
        
        private int _startTouchCount;


        private void Start()
        {
            _startTouchCount =  TouchCount;
        }

        private void DestroyTrigger()
        {
            _collider2D.enabled = false;
            _spriteRenderer.enabled = false;
        }

        public Direction Direction =>  _direction;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                var rand = UnityEngine.Random.Range(0, _audioClips.Length);
                AudioSource.PlayClipAtPoint(_audioClips[rand], Camera.main.transform.position, _settings.SFXVolume);
                _playerMovement = playerMovement;
                DirectionChange();
                TouchCount--;
                _playerMovement.OnPlayerDied -= ResetTrigger;
                _playerMovement.OnPlayerDied += ResetTrigger;
            }
        }

        private void DirectionChange()
        {
            var dir = (int)_direction;
            _playerMovement.SetDirection(dir);
            _localScale = _playerMovement.transform.localScale;
            var invertedLocalScale = _localScale;
            invertedLocalScale.x *= dir;
            _playerMovement.transform.localScale = invertedLocalScale;
        }

        private void ResetTrigger()
        {
            if (!_collider2D.enabled)
            {
                _collider2D.enabled = true;
                _spriteRenderer.enabled = true;
            }
            TouchCount = _startTouchCount;
            _playerMovement.OnPlayerDied -= ResetTrigger;
        }
        
        private void OnDisable()
        {
            if(_playerMovement!=null)
                _playerMovement.OnPlayerDied -= ResetTrigger;
        }
    }
}
