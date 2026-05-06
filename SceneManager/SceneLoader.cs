using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour
{
    // todo - loading screen / progress bar while waiting
    
    [SerializeField]
    private SceneSO initSceneName = default;
    [SerializeField]
    private SceneSO menuSceneName = default;

    private List<Scene> scenesToUnload = new List<Scene>();

    [SerializeField]
    private SceneLoaderChannelSO sceneLoaderChannelSO;

    private void OnEnable() {
        sceneLoaderChannelSO.onSceneChange += LoadScenes;
    }

    private void OnDisable() {
        sceneLoaderChannelSO.onSceneChange -= LoadScenes;
    }

    void Start()
    {
        
    }

    private void LoadScenes(SceneSO[] scenesToLoad)
    {
        AddCurrentScenesToUnload();

        foreach (SceneSO scene in scenesToLoad)
        {
            SceneManager.LoadSceneAsync(scene.sceneName, LoadSceneMode.Additive);
        }

        UnloadScenes();
    }

    private void AddCurrentScenesToUnload()
    {
        for(int i=0; i<SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != initSceneName.sceneName)
            {
                scenesToUnload.Add(scene);
            }
        }
    }

    private void UnloadScenes()
    {
        if(scenesToUnload == null) return;
        
        foreach (Scene scene in scenesToUnload)
        {
            SceneManager.UnloadSceneAsync(scene);
        }
        scenesToUnload.Clear();
    }
}