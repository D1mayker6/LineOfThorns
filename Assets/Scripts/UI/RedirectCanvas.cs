using System;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RedirectCanvas : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private TextMeshProUGUI _title;
        
        private SocialNetwork _socialNetwork;
        private string _url;


        public void Initialize(SocialNetwork social, string url)
        {
          _socialNetwork = social;
          _url = url;
        }
        
        private void Start()
        {
            _continueButton.onClick.AddListener(Continue);
            _backButton.onClick.AddListener(Back);
            _title.text = $"You will be redirected to {_socialNetwork}. \nContinue?";

        }

        private void Continue()
        {
            Destroy(gameObject);
            Application.OpenURL(_url);
        }

        private void Back()
        {
            Destroy(gameObject);
        }
    
    }
}
