using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class SceneLoader : MonoBehaviour {
        private static readonly int Start = Animator.StringToHash("start");
        private static readonly int End = Animator.StringToHash("end");

        private int _scene;
        [SerializeField] private Animator _animator;
        public void LoadNewScene(int scene)
        {
            _scene = scene;
            gameObject.SetActive(true);
            StartCoroutine(LoadNewSceneAsync());
        }


        IEnumerator LoadNewSceneAsync()
        {
            _animator.SetTrigger(Start);
            yield return new WaitForSeconds(1f);
            

            var async = SceneManager.LoadSceneAsync(_scene);
            async.allowSceneActivation = false;

            while (async.progress < 0.9f) {
                yield return null;
            }
            _animator.SetTrigger(End);
            yield return new WaitForSeconds(1f);
            async.allowSceneActivation = true;

        }

    }
}