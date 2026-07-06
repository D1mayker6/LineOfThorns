using System;
using Enums;
using Tools;
using Triggers;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _translationSpeed;
        public float TranslationSpeed => _translationSpeed;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private int _direction;
        [SerializeField] private float _rayDuration;
        [SerializeField] private float _checkRadius;
        [SerializeField] private float _forcePower;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private Transform _collisionCheck;
        
        [SerializeField] private RoomManager _roomManager;

        private Vector2 _startPos;

        private Rigidbody2D _rb;
        public event Action OnPlayerDied;
        
        

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _startPos = transform.position;
            OnPlayerDied += () => {gameObject.SetActive(false); };
        }

        public void ChangeForcePower()
        {
            _forcePower *= -1;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                Jump();
            var ray = new Ray2D(_collisionCheck.position, _collisionCheck.right);
            var hit = Physics2D.Raycast(ray.origin, ray.direction, _rayDuration);
                if(hit.collider && hit.collider.TryGetComponent<Cube>(out var cube))
                    OnPlayerDied?.Invoke();
        }

        private void Jump()
        {   
            if(Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, 1 << LayerMask.NameToLayer("Ground")))
                _rb.AddForce(Vector2.up * _forcePower, ForceMode2D.Impulse);
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
