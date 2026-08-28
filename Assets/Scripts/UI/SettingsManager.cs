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
        
        [SerializeField] private SceneLoader _sceneLoader;
        
        [SerializeField] private DeleteDataPopup _deleteDataPopup;


        private void OnEnable()
        {
            _backButton.onClick.AddListener(Back);
            _deleteDataButton.onClick.AddListener(EnableDeletePopup);
            
            _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
            _sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
            _deleteDataPopup.OnSaveDelete += ClearGameData;
            _musicSlider.value = _settings.MusicVolume;
            _sfxSlider.value = _settings.SFXVolume;
            
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(Back);
            _deleteDataButton.onClick.RemoveListener(ClearGameData);
            
            _musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
            _sfxSlider.onValueChanged.RemoveListener(ChangeSFXVolume);
            
            _dataManager.SaveSettingsData();
        }

        private void Back()
        {
            var loader = Instantiate(_sceneLoader);
            loader.LoadNewScene(0);
        }

        private void EnableDeletePopup()
        {
            _deleteDataPopup.gameObject.SetActive(true);
        }

        private void ClearGameData()
        {
            _dataManager.DeleteGameData();
        }

        private void ChangeMusicVolume(float value)
        {
            _settings.MusicVolume = value;
        }

        private void ChangeSFXVolume(float value)
        {
            _settings.SFXVolume = value;

        }
}
}
