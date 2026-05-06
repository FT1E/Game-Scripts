using Unity.VisualScripting;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    // just for spawning projectiles
    // and keep the other code simple


    [SerializeField]
    private ProjectileManager projectileManager = default;

    [SerializeField]
    private Vector3[] projeectileSpawnOffsets = default;

    [SerializeField]
    private Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    public void SpawnProjectile(int index)
    {
        if(projeectileSpawnOffsets == null) return;
        if(index < 0 || index >= projeectileSpawnOffsets.Length) return;

        Vector3 spawnPosition = transform.position;
        spawnPosition += transform.rotation * projeectileSpawnOffsets[index];
        
        Vector3 direction = enemy.enemyManager.playerInfo.position - spawnPosition;
        direction.y = 0f;
        direction = direction.normalized;

        projectileManager.SpawnProjectile(spawnPosition, direction, enemy.weapon.damage, enemy.weapon.knockbackForce, 6);
    }
}