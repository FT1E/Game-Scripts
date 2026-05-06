using UnityEngine;

public class SceneExit : MonoBehaviour
{
    [SerializeField]
    private SceneLoaderChannelSO sceneLoaderChannelSO;

    [SerializeField]
    private SceneSO[] scenesToLoad;

    [SerializeField]
    private LevelManagerSO levelManagerSO;

    private Collider col;

    void OnEnable()
    {
        col = GetComponent<Collider>();
        col.isTrigger = false;  // disable trigger until level is completed
        levelManagerSO.SetSceneExit(this);
    }

    public void Enable(Material mat)
    {
        col.isTrigger = true;
        GetComponent<MeshRenderer>().material = mat;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name} entered scene exit trigger");
        if (other.gameObject.layer == 6)    // player layer
        {
            sceneLoaderChannelSO.RaiseEvent(scenesToLoad);
        }
    }
}