using System;
using UnityEngine;
using UnityEngine.UI;

public class DeleteDataPopup : MonoBehaviour
{

    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;


    public event Action OnSaveDelete;


    private void OnEnable()
    {
        _yesButton.onClick.AddListener(Yes);
        _noButton.onClick.AddListener(No);
    }


    private void Yes()
    {
        OnSaveDelete?.Invoke();
        gameObject.SetActive(false);
    }

    private void No()
    {
        gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        _yesButton.onClick.RemoveListener(Yes);
        _noButton.onClick.RemoveListener(No);
    }
}
