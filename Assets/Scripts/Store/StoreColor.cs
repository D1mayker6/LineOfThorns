using System;
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
        [SerializeField] private Outline _outline;
        [SerializeField] private bool _isCurrent;

        public bool IsCurrent
        {
            get => _isCurrent;
            set => _isCurrent = value;
        }

        [SerializeField] private Sprite _colorSprite;
        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                _isOpen = value;
                if (_isOpen)
                    _image.sprite = _colorSprite;
            }
        }

        private void SetCurrentColor()
        {
            
        }
    }
}
