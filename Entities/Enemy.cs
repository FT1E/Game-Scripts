using UnityEngine;
using UnityEngine.AI;

public class Enemy : NPCEntity
{

    public EnemyManager enemyManager;   // todo - remove this if it's not used
    public StateMachine stateMachine;

    void Awake()
    {
        _nmAgent = GetComponent<NavMeshAgent>();
        weapon.SetHitLayer(6);
    }


}