using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour {
    
    // todo - object pooling
    private Stack<Enemy> inactiveEnemies;

    [SerializeField]
    private PlayerInfo playerInfo = default;
    // above is for setting target position of NavMeshAgents


    // list of active enemies
    [SerializeField]
    private List<Enemy> activeEnemies;


    void Awake()
    {
        foreach(Transform child in transform)
        {
            activeEnemies.Add(child.GetComponent<Enemy>());
        }
    }

    // todo - spawning
    // todo - despawning/killing enemies
    public void SpawnEnemy(int count)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        
    }

    void Update()
    {
        foreach(Enemy enemy in activeEnemies)
        {
            if(Vector3.Distance(enemy.transform.position, playerInfo.position) < 2f)
            {
                // disable agent
                enemy.nmAgent.isStopped = true;
                // start attack

            }
            else
            {
                // enable agent
                enemy.nmAgent.isStopped = false;
                // follow player
                enemy.nmAgent.destination = playerInfo.position;
            }
        }
    }

}