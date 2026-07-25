using Enums;
using Player;
using UnityEngine;

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
            var pos = new Vector3(playerMovement.transform.localScale.x * dir,playerMovement.transform.localScale.y,playerMovement.transform.localScale.z);
            playerMovement.transform.localScale = pos;
        }
    }
}
