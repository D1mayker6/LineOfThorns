using UnityEngine;

namespace Player
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"детектор столкнулся с {other.gameObject.name}");
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
                _playerMovement.InvokeDeath();
        }
    }
}
