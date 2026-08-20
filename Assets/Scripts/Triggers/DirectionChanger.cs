using System;
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
                _playerMovement = playerMovement;
                DirectionChange();
                TouchCount--;
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
        }

        private void OnDisable()
        {
            _playerMovement.OnPlayerDied -= ResetTrigger;
        }
    }
}
