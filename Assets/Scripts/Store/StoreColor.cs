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
        [SerializeField] private bool _isCurrent;
        [SerializeField] private Button _button;
        
        public event Action<Color, Color> OnColorChosen;

        [SerializeField] private Sprite _colorSprite;
        [SerializeField] private Sprite _chosenSprite;
        public bool IsOpen
        {
            set
            {
                _isOpen = value;
                if (_isOpen)
                    _image.sprite = _colorSprite;
            }
        }

        private void Start()
        {
            _button.onClick.AddListener(SetCurrentColor);
        }

        private void SetCurrentColor()
        {
            OnColorChosen?.Invoke(_backgroundColor, _blockColor);
            GetComponent<Image>().sprite = _chosenSprite;
            
        }
    }
}
