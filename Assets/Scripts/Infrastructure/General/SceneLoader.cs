using System;
using System.Collections;
using Handlers.CoroutineRunnerHandler;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.General
{
    public class SceneLoader
    {
        private readonly ICoroutineRunnerHandler _coroutineRunnerHandler;

        public SceneLoader(ICoroutineRunnerHandler coroutineRunnerHandler) => 
            _coroutineRunnerHandler = coroutineRunnerHandler;

        public void Load(string sceneName, Action onLoad = null) => 
            _coroutineRunnerHandler.CoroutineRunner.StartCoroutine(LoadNextScene(sceneName, onLoad));

        private IEnumerator LoadNextScene(string sceneName, Action onLoad = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoad?.Invoke();
                
                yield break;
            }
            
            AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(sceneName);

            while (!sceneLoading.isDone)
            {
                yield return null;
            }
            
            onLoad?.Invoke();
        }
    }
}