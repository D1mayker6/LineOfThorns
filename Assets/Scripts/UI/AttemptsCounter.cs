using System;
using Player;
using UnityEngine;

namespace UI
{
    public class AttemptsCounter : MonoBehaviour
    {
        private int _attempts = 1;

        public int Attempts
        {
            get => _attempts;
            private set
            {
                _attempts = value;
                OnValueChanged?.Invoke();
            }
        }

        public event Action OnValueChanged;

        [SerializeField] private PlayerMovement _player;

        private void Start()
        {
            _player.OnPlayerDied += AddAttempt;
        }

        private void AddAttempt()
        {
            Attempts++;
        }
    }
}
