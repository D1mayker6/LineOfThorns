using Audio;
using Data;
using UI;
using UnityEngine;

namespace Tools
{
    public class GameLoaderManager : MonoBehaviour
    {

        [SerializeField] private DataManager _dataManager;
        [SerializeField] private SceneLoader _loader;
        [SerializeField] private AudioManager _audioManager;

        private void Start()
        {
            _dataManager.LoadData();
            _audioManager.gameObject.SetActive(true);
            _loader.LoadNewScene(1);
        }
    }
}
