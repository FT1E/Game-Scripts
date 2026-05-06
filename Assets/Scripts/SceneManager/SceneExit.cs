using UnityEngine;

public class SceneExit : MonoBehaviour
{
    [SerializeField]
    private SceneLoaderChannelSO sceneLoaderChannelSO;


    [SerializeField]
    private LevelManagerSO currentLevelManagerSO;
    [SerializeField]
    private LevelManagerSO nextLevelManagerSO;

    [Tooltip("For final level, other scenes to load since no next level.")]
    [SerializeField]
    private SceneSO[] altScenes;

    private Collider col;

    void OnEnable()
    {
        col = GetComponent<Collider>();
        col.isTrigger = false;  // disable trigger until level is completed
        currentLevelManagerSO.SetSceneExit(this);
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
            if(nextLevelManagerSO == null)
            {
                sceneLoaderChannelSO.RaiseEvent(altScenes);
                other.GetComponent<Player>().playerInfo.won = true;
                return;
            }
            sceneLoaderChannelSO.RaiseEvent(nextLevelManagerSO.scenesToLoad);
        }
    }
}