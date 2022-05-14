using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase
{
    public class SceneLoader : MonoBehaviour
    {
        private const string MainScene = "Main";
    
        public void RestartScene()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(MainScene);

            while (!loadSceneAsync.isDone)
                yield return null;
        }
    }
}
