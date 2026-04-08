using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private PlayerInfo playerInfo = default;
    // above is for setting target position of NavMeshAgents


    // list of active enemies
    [SerializeField]
    private List<Enemy> activeEnemies;
    
    // todo - object pooling
    private Stack<Enemy> inactiveEnemies;

    [SerializeField]
    private StateSO initialState;
    // todo - dictionary of initial states for different types of mob enemies
    // todo - so I can reuse this enemy manager to hold different types of mob enemies


    // spawning data
    [SerializeField]
    private readonly float minSpawnDistance = 5f;
    [SerializeField]
    private readonly float maxSpawnDistance = 10f;
    // end spawning data


    [SerializeField]
    private int desiredNumberOfActiveEnemies = 10;
    // the actual number of active enemies is just activeEnemies.Count
    


    void Awake()
    {
        Enemy enemy;
        activeEnemies = new List<Enemy>(transform.childCount * 2);
        inactiveEnemies = new Stack<Enemy>(transform.childCount * 2);

        foreach(Transform child in transform)
        {
            enemy = child.GetComponent<Enemy>();
            enemy.enemyManager = this;
            enemy.stateMachine = new StateMachine(initialState);
            
            // adding first everyone as inactive - then spawning from there
            inactiveEnemies.Push(enemy);
            enemy.gameObject.SetActive(false);
            
        }
    }

    // todo - spawning
    // todo - despawning/killing enemies
    // todo - possible issue - spawning on a non-NavMeshSurface area
    public void SpawnEnemy(int count)
    {
        for(;count > 0; count--)
        {
            Enemy enemy = inactiveEnemies.Pop();
            if(enemy == null) return;   // in case stack is empty

            Vector3 spawnPos = new Vector3(Random.Range(-1f,1f), 0f, Random.Range(-1f,1f));
            spawnPos = spawnPos.normalized * Random.Range(minSpawnDistance, maxSpawnDistance) + playerInfo.position;// around the player

            // todo - if the terrain is gonna be not simple - then do some logistics to see what height should be at certain XZ coordinates
            spawnPos.y = 1f;    // rather have them float a bit and fall down, than accidentally spawn them below ground
            
            enemy.gameObject.SetActive(true);
            enemy.transform.position = spawnPos;
            enemy.knockbackForce = 0f;      // in case it was knocked back before, but this wasn't consumed, like died from the hit
            enemy.ResetHP();
            enemy.stateMachine.Reset(enemy);

            activeEnemies.Add(enemy);
        }
    }

    private bool DeathCheck(Enemy enemy)
    {
        // if it's dead
        if(enemy.Health <= 0 )
        {
            // remove it from active enemies
            activeEnemies.Remove(enemy);
            // then add it to inactive enemies and disable it
            inactiveEnemies.Push(enemy);
            enemy.gameObject.SetActive(false);
            return true;
        }
        return false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        SpawnEnemy(desiredNumberOfActiveEnemies - activeEnemies.Count);

        // todo - think I'll use a state machine after all, makes the code here less cluttered
        // todo - over for some special cases
        Debug.Log($"Number of active enemies:{activeEnemies.Count}");
        int i = 1;
        foreach(Enemy enemy in activeEnemies)
        {
            if(DeathCheck(enemy)) return;

            enemy.isGrounded = Physics.Raycast(enemy.transform.position, Vector3.down, enemy.nmAgent.baseOffset + 0.3f);
            enemy.stateMachine.Update(enemy);

            enemy.stateMachine.PrintStateName($"Enemy {i++} state:");
        }
    }

}