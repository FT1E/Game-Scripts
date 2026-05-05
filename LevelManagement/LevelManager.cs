using UnityEngine;

public class LevelManager : MonoBehaviour
{

    // todo - progress indicator on the pillars with (colored) crystals

    [SerializeField]
    private LevelManagerSO levelManagerSO;

    private int mobsSpawned = 0;
    private int _mobsKilled = 0;
    public int mobsKilled { get { return _mobsKilled; } } 

    private int currentWave = 0;

    [SerializeField]
    private EnemyManager enemyManager;

    private void Awake() {
        levelManagerSO.SetLevelManager(this);
    }

    void Update()
    {
        if(_mobsKilled >= levelManagerSO.mobWaveSizes[currentWave])
        {
            currentWave++;
            if(currentWave == levelManagerSO.mobWaveSizes.Length)
            {
                // end game
                Debug.Log("Level completed!");
                gameObject.SetActive(false);
                return;
            }
        }
        if(mobsSpawned < levelManagerSO.mobWaveSizes[currentWave])
        {
            mobsSpawned += enemyManager.SpawnEnemy(levelManagerSO.mobWaveSizes[currentWave] - mobsSpawned);
        }
    }

    public void IncreaseMobKillCount()
    {
        _mobsKilled++;
    }
}