using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Scene SO", fileName = "SceneSO")]
public class SceneSO : ScriptableObject
{
    [SerializeField]
    private string _sceneName;
    public string sceneName { get { return _sceneName; } }
}