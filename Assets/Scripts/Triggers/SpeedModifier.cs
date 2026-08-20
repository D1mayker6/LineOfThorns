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
                SpeedChangeUp(playerMovement);
                TouchCount--;
                playerMovement.OnPlayerDied += ResetTrigger;
            }
        }

        private void SpeedChangeUp(PlayerMovement playerMovement)
        {
            if(_speed == Speed.Speed)
                playerMovement.TranslationSpeed *= _modifier;
            else
                playerMovement.TranslationSpeed /= _modifier;
        }
        
        private void ResetTrigger()
        {
            if (!_collider2D.enabled)
            {
                _collider2D.enabled = true;
                _spriteRenderer1.enabled = true;
                _spriteRenderer2.enabled = true;
            }
        }
    }
}
