using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Store
{
    public class StoreManager : MonoBehaviour
    {
        [SerializeField] private List<StoreColor> _colors;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _forMoneyButton;
        [SerializeField] private Button _forADButton;


        private void Start()
        {
            _backButton.onClick.AddListener(Back);
            _forMoneyButton.onClick.AddListener(ForMoney);
            _forADButton.onClick.AddListener(ForAD);
            
        }


        private void Back()
        {
            Destroy(gameObject);
        }

        private void ForMoney()
        {
           RandomizeColor(); 
        }

        private void ForAD()
        { 
           RandomizeColor();
        }

        private void RandomizeColor()
        {
            
        }
    }
}
