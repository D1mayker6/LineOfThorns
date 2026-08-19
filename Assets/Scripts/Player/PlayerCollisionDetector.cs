using UnityEngine;

namespace Player
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
                _playerMovement.InvokeDeath();
        }
    }
}
