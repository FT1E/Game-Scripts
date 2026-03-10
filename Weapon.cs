using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

// inheriting classes just set the attackDamage and attackAnimationLength arrays
public abstract class Weapon : MonoBehaviour
{
    private bool attacking = false; // 
    // whether the weapong is currently in attacking animation or not
    private readonly Object attackingLock = new Object();

    private float _damage;
    public float damage { 
        get { return _damage; }
        set { _damage = (value >= 0) ? value : 0; } 
        }

    private Dictionary<int, int> hitObjectsIds = new Dictionary<int, int>();    
    // ids of objects hit during an attack animation,
    // reset to empty each time a new attack animation is started
    


    public void Attack(int attack, Animator animator)
    {
        
    }
    

    // so dmg isn't dealt twice (or many more times)
    // * also might be able to use this to control
    // * in case I do want dmg to be dealt multiple times during the same attack
    public void clearHits()
    {
        hitObjectsIds.Clear();
    }


    public bool setAttackingTrue()
    {
        lock (attackingLock)
        {
            if (attacking) return false;
            attacking = true;
            return true;
        }
    }
    public void setAttackingFalse()
    {
        lock (attackingLock)
        {
            attacking = false;
        }
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
        if (hit.layer != 7) return;

        // check if the enemy was already hit by this attack
        int key = hit.GetInstanceID();
        if (hitObjectsIds.ContainsKey(key)) return;

        // if not
        // add it to hitObjects list - so dmg isn't dealt twice
        hitObjectsIds.Add(key, key);

        // deal damage
        hit.GetComponent<HealthController>().DealDamage(damage);

    }
}
