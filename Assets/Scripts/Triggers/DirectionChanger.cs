using System;
using Enums;
using Player;
using UnityEngine;
using UnityEngine.UIElements;

namespace Triggers
{
    public class DirectionChanger : MonoBehaviour
    {
        [SerializeField] private Direction _direction;
    
        public Direction Direction =>  _direction;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
                DirectionChange(playerMovement);
        }

        private void DirectionChange(PlayerMovement playerMovement)
        {
            var dir = (int)_direction;
            playerMovement.SetDirection(dir);
            playerMovement.transform.localScale = new Vector2(dir, transform.localScale.y);
        }
    }
}
