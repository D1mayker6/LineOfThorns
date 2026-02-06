using System;
using Player;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class AttemptsCounter : MonoBehaviour
    {
        private int _attempts;
    
        public int Attempts => _attempts;

        [SerializeField] private PlayerMovement _player;

        private void Start()
        {
            _player.OnPlayerDied += AddAttempt;
        }

        public void AddAttempt()
        {
            _attempts++;
        }
    }
}
