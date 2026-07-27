using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class SceneLoader : MonoBehaviour {
    
        private int _scene;
        public void LoadNewScene(int scene)
        {
            _scene = scene;
            gameObject.SetActive(true);
            StartCoroutine(LoadNewSceneAsync());
        }


        IEnumerator LoadNewSceneAsync() {

            yield return new WaitForSeconds(3);
        
            var async = SceneManager.LoadSceneAsync(_scene);

            while (!async.isDone) {
                yield return null;
            }

        }

    }
}