using System;
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
        [SerializeField] private float _speedMultiple;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Animator _animator;
        
        [SerializeField] private LayerMask _mask;
        public event Action OnPlayerDied;
        public event Action OnPlayerLevelReached;
        
        private bool _isGrounded;
        
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

        public void ReverseForcePower()
        {
            _forcePower *= -1;
        }

        public void AbsForcePower()
        {
            _forcePower = Mathf.Abs(_forcePower);
        }
        
        public void DiffIncrease()
        {
            _translationSpeed += _speedMultiple;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) || Input.touches.Length > 0)
                Jump();
            
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, 
                1 << LayerMask.NameToLayer("Ground"));
            float relativeVelocity = _rb.linearVelocityY * Mathf.Sign(_rb.gravityScale);
    
            _animator.SetFloat("velocityY", relativeVelocity);
            _animator.SetBool("isGrounded", _isGrounded);
            
            if (Input.GetKeyDown(KeyCode.Escape));
            
        }
        
        private void FixedUpdate()
        {
            Move();
        }


        private void Jump()
        {
            var overlap = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius,
                1 << LayerMask.NameToLayer("Ground"));
            if (overlap && Mathf.Abs(_rb.linearVelocityY) < Mathf.Abs(_forcePower))
            {
                _rb.AddForce(Vector2.up * _forcePower, ForceMode2D.Impulse);
            }
        }
        
        public void InvokeDeath() => OnPlayerDied?.Invoke();

        private void Move()
        {
            var move = Vector2.right * (_direction * _translationSpeed * Time.fixedDeltaTime);
            transform.Translate(move);
        }

        public void SetDirection(int direction)
        {
            _direction = direction;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Trap"))
                InvokeDeath();
            if(other.gameObject.CompareTag("Exit"))
                OnPlayerLevelReached?.Invoke();
        }
    }
}
