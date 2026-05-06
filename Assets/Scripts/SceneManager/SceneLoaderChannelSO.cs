using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Scene Loader Channel SO", fileName = "SceneLoaderChannelSO")]
public class SceneLoaderChannelSO : ScriptableObject
{
    public event UnityAction<SceneSO[]> onSceneChange;
    
    public void RaiseEvent(SceneSO[] scenesToLoad)
    {
        if (onSceneChange != null)
        {
            onSceneChange.Invoke(scenesToLoad);
        }
    }
}