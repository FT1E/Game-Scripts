using UnityEngine;

public class SceneExit : MonoBehaviour
{
    [SerializeField]
    private SceneLoaderChannelSO sceneLoaderChannelSO;

    [SerializeField]
    private SceneSO[] scenesToLoad;

    // when entering inside this space - when collider enabled, change scenes

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name} entered scene exit trigger");
        if (other.gameObject.layer == 6)    // player layer
        {
            sceneLoaderChannelSO.RaiseEvent(scenesToLoad);
        }
    }
}