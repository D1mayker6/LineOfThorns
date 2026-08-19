using Enums;
using Player;
using UnityEngine;

namespace Triggers
{
    public class SpeedModifier : MonoBehaviour
    {
        [SerializeField] private float _modifier = 2f;
        [SerializeField] private Speed _speed;
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

        private void DestroyTrigger() => Destroy(gameObject);
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                SpeedChangeUp(playerMovement);
                TouchCount--;
            }
        }

        private void SpeedChangeUp(PlayerMovement playerMovement)
        {
            if(_speed == Speed.Speed)
                playerMovement.TranslationSpeed *= _modifier;
            else
                playerMovement.TranslationSpeed /= _modifier;
        }
    }
}
