using UnityEngine;

[CreateAssetMenu(fileName = "LevelManagerSO", menuName = "LevelManagerSO", order = 0)]
public class LevelManagerSO : ScriptableObject
{
    [SerializeField]
    public int[] mobWaveSizes;

    private LevelManager _levelManager;
    public LevelManager monoBehaviour { get { return _levelManager; }}

    public float progress {
        get
        {
            if(monoBehaviour == null || mobWaveSizes.Length == 0) return 0f;
            return monoBehaviour.mobsKilled / (float)mobWaveSizes[mobWaveSizes.Length - 1];
        }
    }

    public void SetLevelManager(LevelManager levelManager)
    {
        this._levelManager = levelManager;
    }
    
}