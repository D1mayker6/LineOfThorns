using System;
using Data;
using UnityEngine;
using UnityEngine.UI;

public class DeleteDataPopup : MonoBehaviour
{

    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private SettingsData _settings;


    public event Action OnSaveDelete;


    private void OnEnable()
    {
        _yesButton.onClick.AddListener(Yes);
        _noButton.onClick.AddListener(No);
    }


    private void Yes()
    {
        AudioSource.PlayClipAtPoint(_clickSound, Camera.main.transform.position,_settings.SFXVolume);
        OnSaveDelete?.Invoke();
        gameObject.SetActive(false);
    }

    private void No()
    {
        AudioSource.PlayClipAtPoint(_clickSound, Camera.main.transform.position,_settings.SFXVolume);
        gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        _yesButton.onClick.RemoveListener(Yes);
        _noButton.onClick.RemoveListener(No);
    }
}
