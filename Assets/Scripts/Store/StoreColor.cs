using System;
using UnityEngine;
using UnityEngine.UI;

namespace Store
{
    public class StoreColor : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private Color _backgroundColor;
        [SerializeField] private Color _blockColor;
        [SerializeField] private Color _UIColor;
        [SerializeField] private Image _image;
        [SerializeField] private bool _isOpen;
        [SerializeField] private bool _isCurrent;
        [SerializeField] private Button _button;
        
        public event Action<Color, Color, int> OnColorChosen;

        [SerializeField] private Sprite _colorSprite;
        public bool IsOpen
        {
            private get => _isOpen;
            set
            {
                _isOpen = value;
                if (_isOpen)
                {
                    _image.sprite = _colorSprite;
                    CheckInteractable();
                }
            }
        }

        private void Start()
        {
            _button.onClick.AddListener(SetCurrentColor);
            CheckInteractable();
        }

        private void CheckInteractable()
        {
            if(!IsOpen)
                _button.interactable = false;
            else 
                _button.interactable = true;
        }

        private void SetCurrentColor() => OnColorChosen?.Invoke(_backgroundColor, _blockColor, _id);
    }
}
