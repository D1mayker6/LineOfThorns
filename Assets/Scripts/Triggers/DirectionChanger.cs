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

        public Direction Direction =>  _direction;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                DirectionChange(playerMovement);
                TouchCount--;
                if(playerMovement && gameObject)
                    playerMovement.OnPlayerDied += ResetTouches;
            }
        }

        private void DirectionChange(PlayerMovement playerMovement)
        {
            var dir = (int)_direction;
            playerMovement.SetDirection(dir);
            var pos = new Vector3(playerMovement.transform.localScale.x * dir,playerMovement.transform.localScale.y,playerMovement.transform.localScale.z);
            playerMovement.transform.localScale = pos;
        }

        private void ResetTouches()
        {
            TouchCount = _startTouchCount;
        }
    }
}
