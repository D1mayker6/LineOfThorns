using System;
using Enums;
using Player;
using UnityEngine;

namespace Triggers
{
    public class SpeedModifier : MonoBehaviour
    {
        [SerializeField] private float _modifier = 2f;
        [SerializeField] private Speed _speed;
        [SerializeField] private int _touchCount = 1;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private SpriteRenderer _spriteRenderer1;
        [SerializeField] private SpriteRenderer _spriteRenderer2;
        [SerializeField] private PlayerMovement _playerMovement;

        public int TouchCount
        {
            get => _touchCount;
            set
            {
                _touchCount = value;
                if (_touchCount <= 0)
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
            _spriteRenderer1.enabled = false;
            _spriteRenderer2.enabled = false;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                _playerMovement = playerMovement;
                SpeedChangeUp();
                TouchCount--;
                _playerMovement.OnPlayerDied -= ResetTrigger;
                _playerMovement.OnPlayerDied += ResetTrigger;
            }
        }

        private void SpeedChangeUp()
        {
            if(_speed == Speed.Fast)
                _playerMovement.TranslationSpeed *= _modifier;
            else
                _playerMovement.TranslationSpeed /= _modifier;
        }
        
        private void ResetTrigger()
        {
            if (!_collider2D.enabled)
            {
                _collider2D.enabled = true;
                _spriteRenderer1.enabled = true;
                _spriteRenderer2.enabled = true;
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
