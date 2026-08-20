using System;
using Enums;
using UnityEngine;
namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _translationSpeed;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private int _direction;
        [SerializeField] private float _rayDuration;
        [SerializeField] private float _checkRadius;
        [SerializeField] private float _forcePower;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private Transform _spawnpoint;
        [SerializeField] private float _speedMultiple;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Animator _animator;
        
        [SerializeField] private LayerMask _mask;
        public event Action OnPlayerDied;
        public event Action OnPlayerLevelReached;
        
        private bool _isGrounded;

        private float _baseSpeed;
        
        public float TranslationSpeed
        {
            get => _translationSpeed;
            set
            {
                if (value <= 0)
                    return;
                _translationSpeed = value;
            }
        }

        private void OnEnable()
        {
            OnPlayerLevelReached += RestorePlayer;
            OnPlayerDied += ResetPlayerStates;
            _baseSpeed = _translationSpeed;
            transform.position = _spawnpoint.position;
        }

        private void ResetPlayerStates()
        {
            _translationSpeed = _baseSpeed;
            _rb.gravityScale = Mathf.Abs(_rb.gravityScale);
            AbsForcePower();
            SetDirection((int)Direction.Right);

        }

        private void RestorePlayer() => _translationSpeed = _baseSpeed;

        public void ReverseForcePower() => _forcePower *= -1;
        

        public void AbsForcePower() => _forcePower = Mathf.Abs(_forcePower);
        private void Update()
        {
            if (Input.GetMouseButton(0) || Input.touches.Length > 0)
                Jump();
            
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, 
                _mask);
            float relativeVelocity = _rb.linearVelocityY * Mathf.Sign(_rb.gravityScale);

            var hit = Physics2D.Raycast(_groundCheck.position, Vector2.right * _direction, _rayDuration, _mask);
            if(hit.collider)
                OnPlayerDied?.Invoke();
    
            _animator.SetFloat("velocityY", relativeVelocity);
            _animator.SetBool("isGrounded", _isGrounded);
            
        }
        
        private void FixedUpdate()
        {
            Move();
        }


        private void Jump()
        {
            var overlap = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius,
                1 << LayerMask.NameToLayer("Ground"));
            if (overlap)
                _rb.linearVelocity = new Vector2(_rb.linearVelocityX, _forcePower);
        }
        
        public void InvokeDeath() => OnPlayerDied?.Invoke();

        private void Move()
        {
            var move = Vector2.right * (_direction * _translationSpeed * Time.fixedDeltaTime);
            transform.Translate(move);
            Debug.Log(move);
        }

        public void SetDirection(int direction) => _direction = direction;
        
        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.gameObject.CompareTag("Trap"))
                InvokeDeath();
            if (other.gameObject.CompareTag("Exit"))
            {
                OnPlayerLevelReached?.Invoke();
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_groundCheck == null) return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _checkRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_groundCheck.position, _groundCheck.position + new Vector3(_rayDuration * _direction, 0,  0));
        }

        private void OnDisable()
        {
            OnPlayerLevelReached -= RestorePlayer;
        }
    }
}
