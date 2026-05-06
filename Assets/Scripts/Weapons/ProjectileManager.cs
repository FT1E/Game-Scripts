using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    
    // todo - different stacks for different types of projectiles if needed
    private Stack<Projectile> disabledProjectiles = new Stack<Projectile>();

    void Awake()
    {
        disabledProjectiles = new Stack<Projectile>(transform.childCount * 2);
        foreach(Transform child in transform)
        {
            Projectile projectile = child.GetComponent<Projectile>();
            projectile.poolManager = this;
            child.gameObject.SetActive(false);
            disabledProjectiles.Push(projectile);
        }
    }

    public void DisableProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.clearHits();
        disabledProjectiles.Push(projectile);
    }

    // for calculating the correct position, experiment in the scene while the entity is in animation
    public void SpawnProjectile(Vector3 position, Vector3 direction, float damage, float knockbackForce, int hitLayer)
    {
        if (disabledProjectiles.Count == 0)
        {
            Debug.LogWarning("Projectile stack empty!");
            return;
        }

        Projectile projectile = disabledProjectiles.Pop();
        projectile.transform.position = position;
        projectile.transform.forward = direction;
        projectile.SetDirection(direction);
        projectile.SetDamage(damage);
        projectile.SetKnockback(knockbackForce);
        projectile.SetHitLayer(hitLayer);
        projectile.gameObject.SetActive(true);
    }
}