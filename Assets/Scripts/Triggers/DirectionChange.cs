using Enums;
using UnityEngine;

namespace Triggers
{
    public class DirectionChange : MonoBehaviour
    {
        [SerializeField] private Direction _direction;
    
        public Direction Direction =>  _direction;
    }
}
