using System;
using System.Collections;
using Audio;
using Data;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Tools
{
    public class GameLoaderManager : MonoBehaviour
    {

        [SerializeField] private DataManager _dataManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private Animator _screenAnimator;
        [SerializeField] private GameObject _loadScreen;
        
        private readonly int End = Animator.StringToHash("end");

        private void Start()
        {
            StartCoroutine(LoadSequence());
        }


        IEnumerator LoadSequence()
        {
            Application.targetFrameRate = 60;
            _dataManager.LoadData();
            _audioManager.gameObject.SetActive(true);
            var async = SceneManager.LoadSceneAsync(1);
            async.allowSceneActivation = false;
            _videoPlayer.Prepare();
            while (!_videoPlayer.isPrepared)
                yield return null;
            
            _screenAnimator.SetTrigger(End);
            yield return new WaitForSeconds(1f);
            _loadScreen.gameObject.SetActive(false);
            
            _videoPlayer.Play();
            while (_videoPlayer.isPlaying)
                yield return null;
            
            
            async.allowSceneActivation = true;
            
        }
    }
}
