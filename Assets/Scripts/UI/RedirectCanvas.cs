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
        [SerializeField] private AudioClip _clickSound;
        
        private SocialNetwork _socialNetwork;
        private string _url;


        public void Initialize(SocialNetwork social, string url)
        {
          _socialNetwork = social;
          _url = url;
        }
        
        private void Start()
        {
            _title.text = $"You will be redirected to {_socialNetwork}. \nContinue?";
        }
        
        private void OnEnable()
        {
            _continueButton.onClick.AddListener(Continue);
            _backButton.onClick.AddListener(Back);
        }

        private void Continue()
        {
            AudioSource.PlayClipAtPoint(_clickSound, transform.position);
            Destroy(gameObject);
            Application.OpenURL(_url);
        }

        private void Back()
        {
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            AudioSource.PlayClipAtPoint(_clickSound, transform.position);
            _continueButton.onClick.RemoveListener(Continue);
            _backButton.onClick.RemoveListener(Back);
            
        }
    }
}
