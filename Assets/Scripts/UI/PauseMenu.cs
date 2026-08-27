using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private SceneLoader _sceneLoaderPrefab;


    private void Awake()
    {
        _backButton.onClick.AddListener(BackButton_OnClick);
        _mainMenuButton.onClick.AddListener(MainMenuButton_OnClick);
    }

    private void BackButton_OnClick()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1; 
    }

    private void MainMenuButton_OnClick()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        var loader = Instantiate(_sceneLoaderPrefab);
        loader.LoadNewScene(0);
    }
}
