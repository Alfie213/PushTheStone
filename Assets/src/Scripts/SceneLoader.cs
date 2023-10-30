using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [Header("Scene names")]
    [SerializeField] private string gameSceneName;
    [SerializeField] private string mainMenuSceneName;
    
    [Header("Loading setup")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;

    public void LoadGameScene()
    {
        StartCoroutine(LoadSceneAsync(gameSceneName));
    }
    
    public void LoadMainMenuScene()
    {
        StartCoroutine(LoadSceneAsync(mainMenuSceneName));
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += Handle_sceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= Handle_sceneLoaded;
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            loadingSlider.value = Mathf.Clamp01(operation.progress / 0.9f);

            yield return null;
        }
    }

    private void Handle_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == gameSceneName)
        {
            EnvironmentEventBus.OnGameSceneLoad.Publish();
        }
    }
}
