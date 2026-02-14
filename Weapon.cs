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

    private int currentAttack = -1;    // != -1 if the wapon is in attacking animation
    // each attacking animation may deal different dmg hence this variable

    private Dictionary<int, int> hitObjectsIds = new Dictionary<int, int>();    // ids of objects hit during an attack animation, reset to empty each time a new attack animation is started
    // may change the type to some kind of dictionary

    protected float[] attackDamage;   // how much damage each attack does
    // attack[i] does attackDamage[i] damage
    protected float[] attackAnimationLength;  // how long attack animation lasts
                                              // kinda like above, but for length of animation

    private IEnumerator coroutine;

    private Animator animator = null;

    public void Attack(int attack, Animator animator)
    {
        if (this.animator == null) this.animator = animator;
        coroutine = AttackCoroutine(attack);
        StartCoroutine(coroutine);
    }
    private IEnumerator AttackCoroutine(int attack)
    {
        // argument is which attack to perform

        // out of bounds check
        if (attack < 0 || attack >= attackDamage.Length) yield break;

        currentAttack = attack;

        // see if an attack is currently being performed
        // todo - may change this maybe for button mashing to work
        // like wait at max a second or 2 to perform the attack
        if (!setAttackingTrue()) yield break;


        // set hitObjectsId to empty list
        hitObjectsIds.Clear();

        // start animation  
        animator.SetTrigger($"Attack{currentAttack}");

        // attacking set to true
        yield return new WaitForSeconds(attackAnimationLength[currentAttack]);
        setAttackingFalse();
    }

    private bool setAttackingTrue()
    {
        lock (attackingLock)
        {
            if (attacking) return false;
            attacking = true;
            return true;
        }
    }
    private void setAttackingFalse()
    {
        lock (attackingLock)
        {
            attacking = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OnCollisionEnter called");
        onHit(collision.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter called");
        onHit(other.gameObject);
    }

    private void onHit(GameObject hit)
    {
        if (!attacking) return;
        if (hit.layer != 7) return;
        if (currentAttack == -1) return;

        // check if the enemy was already hit by this attack
        int key = hit.GetInstanceID();
        if (hitObjectsIds.ContainsKey(key)) return;

        // if 
        // add it to hitObjects list - so dmg isn't dealt twice
        hitObjectsIds.Add(key, key);

        // deal damage
        hit.GetComponent<HealthController>().DealDamage(attackDamage[currentAttack]);

    }
}
