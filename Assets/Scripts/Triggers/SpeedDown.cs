using Player;
using UnityEngine;

namespace Triggers
{
    public class SpeedDown : MonoBehaviour
    {
        private float _modifier = 2f;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                SpeedChangeDown(playerMovement);
            }
        }

        private void SpeedChangeDown(PlayerMovement playerMovement)
        {
            playerMovement.TranslationSpeed /= _modifier;
        }
    }
}
