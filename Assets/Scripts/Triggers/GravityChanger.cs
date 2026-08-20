using System;
using Enums;
using Player;
using UnityEngine;

namespace Triggers
{
    public class GravityChanger : MonoBehaviour
    {
    
        [SerializeField] private GravityValue _gravityValue;
        [SerializeField] private ParticleSystem _particlePrefab;
        [SerializeField] private float _impulse = 200f;
        [SerializeField] private int _touchCount = 1;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PlayerMovement _playerMovement;

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
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                _playerMovement = playerMovement;
                GravityChange();
                TouchCount--;
                _playerMovement.OnPlayerDied -= ResetTrigger;
                _playerMovement.OnPlayerDied += ResetTrigger;
            }
        }

        private void GravityChange()
        {
            var rb = _playerMovement.GetComponent<Rigidbody2D>();
            var intGravityValue = (int)_gravityValue;
            rb.gravityScale = Mathf.Abs(rb.gravityScale) * intGravityValue;
            var particle = Instantiate(_particlePrefab, _playerMovement.transform.position, Quaternion.identity);
            if (_gravityValue == GravityValue.Down)
            {
                _impulse *= -1f;
                var main = particle.main;
                main.startSpeedMultiplier *= -1f;
            }
            rb.AddForceY(_impulse,ForceMode2D.Impulse);
            _playerMovement.ReverseForcePower();

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
