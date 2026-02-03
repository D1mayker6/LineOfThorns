using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _translationSpeed;
        [SerializeField] private float _maxVelocity;
        [SerializeField] private float _forcePower;
        [SerializeField] private float _checkRadius;
        [SerializeField] private Transform _groundCheck;

        private Vector2 _startPos;

        public event Action OnPlayerDied; 
    
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _startPos = transform.position;
            OnPlayerDied += Respawn;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                Jump();
        }

        private void Jump()
        {   
            if(Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, 1 << LayerMask.NameToLayer("Ground")))
                _rb.AddForce(Vector2.up * _forcePower, ForceMode2D.Impulse);
        }

        private void Move()
        {
            var move = Vector2.right * (_translationSpeed * Time.fixedDeltaTime);
            transform.Translate(move);
        }

        private void Respawn()
        {
            transform.position = _startPos;
            Debug.Log("Плеер  здох!");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Trap>(out var trap))
                OnPlayerDied?.Invoke();
        }
    }
}
