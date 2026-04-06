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

    [SerializeField]
    private StateSO initialState;
    // todo - dictionary of initial states for different types of mob enemies
    // todo - so I can reuse this enemy manager to hold different types of mob enemies


    void Awake()
    {
        Enemy enemy;
        foreach(Transform child in transform)
        {
            enemy = child.GetComponent<Enemy>();
            enemy.enemyManager = this;
            enemy.stateMachine = new StateMachine(initialState);
            activeEnemies.Add(enemy);
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
        // todo - think I'll use a state machine after all, makes the code here less cluttered
        // todo - over for some special cases
        foreach(Enemy enemy in activeEnemies)
        {
            enemy.isGrounded = Physics.Raycast(enemy.transform.position, Vector3.down, enemy.nmAgent.baseOffset + 0.3f);
            enemy.stateMachine.Update(enemy);

            
            // enemy.stateMachine.PrintStateName();
        }
    }

}