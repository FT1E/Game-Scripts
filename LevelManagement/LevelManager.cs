using UnityEngine;

public class LevelManager : MonoBehaviour
{

    // todo - progress indicator on the pillars with (colored) crystals

    [SerializeField]
    private LevelManagerSO levelManagerSO;

    private int mobsSpawned = 0;
    private int mobsKilled = 0;

    private int currentWave = 0;

    [SerializeField]
    private EnemyManager enemyManager;

    private void Awake() {
        levelManagerSO.SetLevelManager(this);
    }

    void Update()
    {
        if(mobsKilled >= levelManagerSO.mobWaveSizes[currentWave])
        {
            currentWave++;
        }
        if(mobsSpawned < levelManagerSO.mobWaveSizes[currentWave])
        {
            mobsSpawned += enemyManager.SpawnEnemy(levelManagerSO.mobWaveSizes[currentWave] - mobsSpawned);
        }
    }

    public void IncreaseMobKillCount()
    {
        mobsKilled++;
    }
}