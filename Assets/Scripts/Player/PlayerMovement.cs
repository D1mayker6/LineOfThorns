using System;
using Enums;
using Tools;
using Triggers;
using UI;
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
        [SerializeField] private Transform _collisionCheck;
        [SerializeField] private float _speedMultiple;
        
        [SerializeField] private RoomManager _roomManager;
        [SerializeField] private ScoreCounter _scoreCounter;

        private Vector2 _startPos;
        
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Animator _animator;
        public event Action OnPlayerDied;
        
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
        
        

        private void Start()
        {
            _startPos = transform.position;
            OnPlayerDied += () => {gameObject.SetActive(false); };
            _scoreCounter.OnDiffReached += DiffIncrease;
        }

        public void ReverseForcePower(int direction)
        {
            _forcePower *= -1;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void DiffIncrease()
        {
            _translationSpeed += _speedMultiple;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) ||
               Input.touches.Length > 0)
                Jump();
            var ray = new Ray2D(_collisionCheck.position, _collisionCheck.right);
            var hit = Physics2D.Raycast(ray.origin, ray.direction, _rayDuration);
                if(hit.collider && hit.collider.TryGetComponent<Cube>(out var cube))
                    OnPlayerDied?.Invoke();
                
        }

        private void Jump()
        {
            var overlap = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius,
                1 << LayerMask.NameToLayer("Ground"));
            if (overlap && Mathf.Abs( _rb.linearVelocityY) < Mathf.Abs(_forcePower))
            {
                _rb.AddForce(Vector2.up * _forcePower, ForceMode2D.Impulse);
                _animator.SetTrigger("Jump");
            }
        }

        private void Move()
        {
            var move = Vector2.right * (_direction * _translationSpeed * Time.fixedDeltaTime);
            transform.Translate(move);
        }

        private void Respawn()
        {
            transform.position = _startPos;
            Debug.Log("Плеер  здох!");
        }

        public void SetDirection(int direction)
        {
            _direction = direction;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Trap>(out var trap))
                OnPlayerDied?.Invoke();
            if(other.gameObject.TryGetComponent<TeleportRoom>(out var teleportRoom))
                _roomManager.GoToNextLevel();
        }
    }
}
