using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    [SerializeField]
    private float spawnRadius = 3f;

    [SerializeField]
    private float spawnRate = 1f;   // how many seconds between spawns

    private float timeSinceLastSpawn = 0f;

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
    }


    public bool Available()
    {
        return timeSinceLastSpawn >= spawnRate;
    }
    public Vector3 GetSpawnPosition()
    {
        timeSinceLastSpawn = 0f;
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        // 1f on y, so enemy doesn't get stuck, will float for a very little bit
        return transform.position + new Vector3(randomCircle.x, 1f, randomCircle.y);
    }
    
}