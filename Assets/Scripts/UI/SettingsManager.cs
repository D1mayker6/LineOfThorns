using System;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private DataManager _dataManager;
        [SerializeField] private SettingsData _settings;

        [SerializeField] private Button _backButton;
        [SerializeField] private Button _deleteDataButton;

        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;


        private void OnEnable()
        {
            throw new NotImplementedException();
        }

        private void OnDisable()
        {
            throw new NotImplementedException();
        }
}
}
