using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

// inheriting classes just set the attackDamage and attackAnimationLength arrays
public class Weapon : MonoBehaviour
{
    private bool attacking = false; // 
    // whether the weapong is currently in attacking animation or not

    private float _damage;
    public float damage { 
        get { return _damage; }
    }
    

    private float _knockbackForce;
    public float knockbackForce { 
        get { return _knockbackForce; }
    }
    

    public bool cancelAttacks = false;

    private HashSet<int> hitObjectsIds = new HashSet<int>();    
    // ids of objects hit during an attack animation,
    // reset to empty each time a new attack animation is started
    // so dmg isn't dealt twice (or many more times)
    // * also might be able to use this to control
    // * in case I do want dmg to be dealt multiple times during the same attack

    private int hitLayer;
    // player wants to attack enemy
    // enemy wants to attack player
    // so a variable to do less checking
    // only set once

    public virtual void SetHitLayer(int layer)
    {
        hitLayer = layer;
    }
    public virtual void SetDamage(float damage)
    {
        _damage = damage;
    }
    public virtual void SetKnockback(float knockbackForce)
    {
        _knockbackForce = knockbackForce;
    }

    public virtual void clearHits()
    {
        hitObjectsIds.Clear();
    }


    public virtual void setAttackingTrue()
    {
        attacking = true;
        clearHits();
    }
    public virtual void setAttackingFalse()
    {
        attacking = false;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //Debug.Log("OnCollisionEnter called");
    //    onHit(collision.gameObject);
    //}
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter called");
        onHit(other.gameObject);
    }

    private void onHit(GameObject hit)
    {
        if (!attacking) return;
        if (hit.layer != hitLayer) return;

        // check if the enemy was already hit by this attack
        int key = hit.GetInstanceID();
        if (hitObjectsIds.Contains(key)) return;

        // if not
        // add it to hitObjects list - so dmg isn't dealt twice
        hitObjectsIds.Add(key);

        // deal damage
        Entity entity = hit.GetComponent<Entity>();
        entity.DealDamage(damage);
        entity.knockbackForce = knockbackForce;

        if(cancelAttacks) entity.cancelAttack();

    }
}
