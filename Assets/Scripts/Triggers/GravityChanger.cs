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
            _startTouchCount = TouchCount;
        }

        private void DestroyTrigger() => Destroy(gameObject);
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                GravityChange(playerMovement);
                TouchCount--;
                if(playerMovement && gameObject)
                    playerMovement.OnPlayerDied += ResetTouches;
            }
        }

        private void GravityChange(PlayerMovement playerMovement)
        {
            var rb = playerMovement.GetComponent<Rigidbody2D>();
            var intGravityValue = (int)_gravityValue;
            rb.gravityScale = Mathf.Abs(rb.gravityScale) * intGravityValue;
            var particle = Instantiate(_particlePrefab, playerMovement.transform.position, Quaternion.identity);
            if (_gravityValue == GravityValue.Down)
            {
                _impulse *= -1f;
                var main = particle.main;
                main.startSpeedMultiplier *= -1f;
            }
            rb.AddForceY(_impulse,ForceMode2D.Impulse);
            playerMovement.ReverseForcePower();

        }
        
        private void ResetTouches()
        {
            TouchCount = _startTouchCount;
        }
    }
}
