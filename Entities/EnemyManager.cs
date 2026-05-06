using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private LevelManagerSO levelManagerSO;

    [SerializeField]
    public bool trackAttackRate = false;

    [SerializeField]
    public PlayerInfo playerInfo = default;
    // above is for setting target position of NavMeshAgents


    // list of active enemies
    [SerializeField]
    private List<Enemy> activeEnemies;
    
    public int AliveMobCount { get { return activeEnemies.Count; } }
    //  - object pooling
    private Queue<Enemy> inactiveEnemies;       // queue even if it's inneficient to have more variety, not just the top 2,3 mobs

    [SerializeField]
    private StateSO initialState;
    // todo - dictionary of initial states for different types of mob enemies
    // * - so I can reuse this enemy manager to hold different types of mob enemies

    [SerializeField]
    private string[] tagsInitState;
    [SerializeField]
    private StateSO[] initStates;

    private Dictionary<string, StateSO> initStateDict;


    // spawning data
    [SerializeField]
    private SpawnPoint[] spawnPoints;
    private Queue<SpawnPoint> spawnPointQueue;
    // end spawning data


    [SerializeField]
    private int desiredNumberOfActiveEnemies = 10;
    // the actual number of active enemies is just activeEnemies.Count
    


    void Awake()
    {
        spawnPointQueue = new Queue<SpawnPoint>(spawnPoints);

        // lazy, but idc for now
        initStateDict = new Dictionary<string, StateSO>();
        for(int i=0; i<tagsInitState.Length; i++)
        {
            initStateDict[tagsInitState[i]] = initStates[i];
        }

        activeEnemies = new List<Enemy>(transform.childCount * 2);
        inactiveEnemies = new Queue<Enemy>(transform.childCount * 2);

        // bit of randomization
        Enemy[] children = transform.GetComponentsInChildren<Enemy>();
        MyPhysics.Shuffle(children);

        foreach(Enemy enemy in children)
        {
            enemy.enemyManager = this;
            enemy.stateMachine = new StateMachine(initStateDict[enemy.tag]);
            
            // adding first everyone as inactive - then spawning from there
            inactiveEnemies.Enqueue(enemy);
            enemy.gameObject.SetActive(false);
            
        }
    }

    private SpawnPoint getNextSpawnPoint()
    {
        SpawnPoint spawnPoint = spawnPointQueue.Dequeue();
        spawnPointQueue.Enqueue(spawnPoint);
        return spawnPoint;
    }

    // todo - possible issue - spawning on a non-NavMeshSurface area
    public int SpawnEnemy(int count)
    {
        int spawned = 0;
        for(;count > 0; count--)
        {
            if(inactiveEnemies == null || inactiveEnemies.Count == 0) break;
            Enemy enemy = inactiveEnemies.Dequeue();

            SpawnPoint spawnPoint = getNextSpawnPoint();
            if (!spawnPoint.Available())
            {
                inactiveEnemies.Enqueue(enemy);   // put it back
                // will skip this time and try again next frame
                continue;
            }   
            Vector3 spawnPos = spawnPoint.GetSpawnPosition();

            //* - the terrain is gonna be simple
            spawnPos.y = 1f;    // rather have them float a bit and fall down, than accidentally spawn them below ground
            
            enemy.gameObject.SetActive(true);
            enemy.transform.position = spawnPos;
            enemy.knockbackForce = 0f;      // in case it was knocked back before, but this wasn't consumed, like died from the hit
            enemy.ResetHP();
            enemy.stateMachine.Reset(enemy);

            activeEnemies.Add(enemy);
            spawned++;
        }
        return spawned;
    }

    private bool DeathCheck(Enemy enemy)
    {
        // if it's dead
        if(enemy.Health <= 0 )
        {
            // remove it from active enemies
            activeEnemies.Remove(enemy);
            // then add it to inactive enemies and disable it
            inactiveEnemies.Enqueue(enemy);
            enemy.gameObject.SetActive(false);
            levelManagerSO.monoBehaviour.IncreaseMobKillCount();
            return true;
        }
        return false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        // SpawnEnemy(desiredNumberOfActiveEnemies - activeEnemies.Count);
        // todo - group behvaiour for mobs
        Debug.Log($"Number of active enemies:{activeEnemies.Count}");
        int i = 1;
        foreach(Enemy enemy in activeEnemies.ToList())
        {
            if (trackAttackRate) enemy.timeSinceLastAtk += Time.deltaTime;
            if(DeathCheck(enemy)) continue;

            // doing below for ground checking - and also setting y velocity, 
            // so it doesn't go through the ground 
            // and so it doesn't speed up - slow down - speed up - slow down 
            // - which is what would happen if i just set isGrounded and not affect velocityVector.y 
            Ray ray = new Ray(enemy.transform.position, Vector3.down);
            if(enemy.isGrounded = Physics.Raycast( ray, out RaycastHit hitinfo, 0.01f + enemy.nmAgent.baseOffset - enemy.velocityVector.y * Time.deltaTime))
            {
                enemy.velocityVector.y = -hitinfo.distance;
            }

            enemy.stateMachine.Update(enemy);

            // enemy.stateMachine.PrintStateName($"Enemy {i++} state:");
        }
    }

}