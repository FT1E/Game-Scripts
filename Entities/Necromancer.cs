using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Necromancer : Enemy
{
    [SerializeField]
    private EnemyManager spawnPool;
    [SerializeField]
    private float spawnRate = 60f;  // how many seconds between spawns
    private float spawnTimer = 50f; // default value so it spawns 10s after start

    [SerializeField]
    private int maxSpawnedEnemies = 5;
    [SerializeField]
    private int minSpawnedEnemies = 1;

    [SerializeField]
    private ParticleSystem particles;   // to play while spawning

    [Tooltip("How long to stay still after spawning.")]
    [SerializeField]
    private float spawnDelay = 5f;


    private bool _isSpawning = false;
    public bool isSpawning { get { return _isSpawning; } }

    void Awake()
    {
        _nmAgent = GetComponent<NavMeshAgent>();
        weapon.SetHitLayer(6);
    }

    void Update()
    {
        // every 60s spawn more enemies
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            StartCoroutine(SpawnDelay());
        }
    }

    private IEnumerator SpawnDelay()
    {
        spawnTimer = 0f;

        _isSpawning = true;
        
        // cancel any attack and go into spawn animation
        // copied from CancelAttack
        // not calling it so it doesn't go into hit animation
        attackPerformed = true;
        weapon.SetDamage(0f);
        weapon.SetKnockback(0f);
        weapon.setAttackingFalse();

        stateMachine.Reset(this);

        particles.Play();
        animator.SetBool("SpawnSpell", true);
        spawnPool.SpawnEnemy(Random.Range(minSpawnedEnemies, maxSpawnedEnemies + 1));
        yield return new WaitForSeconds(spawnDelay); // adjust this delay as needed
        
        particles.Stop();
        animator.SetBool("SpawnSpell", false);
        spawnTimer = 0f;

        _isSpawning = false;
    }

}