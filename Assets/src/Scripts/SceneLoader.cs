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

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadSceneAsync(gameSceneName));
    }
    
    public void LoadMainMenuScene()
    {
        StartCoroutine(LoadSceneAsync(mainMenuSceneName));
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
}
