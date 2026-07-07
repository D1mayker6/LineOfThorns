using Player;
using UnityEngine;

namespace Triggers
{
    public class SpeedUp : MonoBehaviour
    {
        private float _modifier = 2f;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                SpeedChangeUp(playerMovement);
            }
        }

        private void SpeedChangeUp(PlayerMovement playerMovement)
        {
            playerMovement.TranslationSpeed *= _modifier;
        }
    }
}
