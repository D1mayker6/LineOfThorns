using UnityEngine;
using UnityEngine.UI;

namespace Store
{
    public class StoreColor : MonoBehaviour
    {
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Color _blockColor;
        [SerializeField] private Image _image;
        [SerializeField] private bool _isOpen;

        [SerializeField] private Sprite _colorSprite;
    }
}
