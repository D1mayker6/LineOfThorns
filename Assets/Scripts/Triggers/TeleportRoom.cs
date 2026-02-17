using Player;
using UnityEngine;

namespace Triggers
{
    public class TeleportRoom : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<PlayerMovement>(out var player))
                GoToNextRoom();
        }

        private void GoToNextRoom()
        {
            
        }
    }
}
