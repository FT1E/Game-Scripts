using UnityEngine;
using UnityEngine.AI;

public class Enemy : NPCEntity
{

    public EnemyManager enemyManager;   // todo - remove this if it's not used
    public StateMachine stateMachine;
    [SerializeField]
    public ProjectileManager projectilePoolManager = default;

    void Awake()
    {
        _nmAgent = GetComponent<NavMeshAgent>();
        weapon.SetHitLayer(6);
    }

    public void SpawnProjectile()
    {
        Debug.Log("shooting projectile");
        if(projectilePoolManager == null) return;
        
        // todo - find a workaround for passing a vector3 for position delta
        // * for different type of projectile shooting animations and also for different types of projectiles
        Vector3 spawnPosition = transform.position + new Vector3(0.328999996f,0.85799998f,1.17700005f);
        Vector3 direction = enemyManager.playerInfo.position - spawnPosition;
        direction.y = 0f;
        direction = direction.normalized;
        projectilePoolManager.SpawnProjectile(spawnPosition, direction, weapon.damage, weapon.knockbackForce, 6);
    }

}