using UnityEngine;

[CreateAssetMenu(fileName = "LevelManagerSO", menuName = "LevelManagerSO", order = 0)]
public class LevelManagerSO : ScriptableObject
{
    [SerializeField]
    public int[] mobWaveSizes;

    private LevelManager _levelManager;
    public LevelManager LevelManager { get { return _levelManager; }}


    public void SetLevelManager(LevelManager levelManager)
    {
        this._levelManager = levelManager;
    }
    
}