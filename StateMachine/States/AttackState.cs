using System;
using System.Collections;
using UnityEngine;

public class AttackState : MState
{
    // TODO - may add attack cancelling mechanics - or just use shorter animations

    // Component variables used
    private Character character;
    private Animator animator;
    private Weapon weapon;
    // end Component variables

    // damage
    private float damage;

    // animation data
    private float clipLength;
    private String animatorParam;    
    // * will use a bool animator param for now - might change it to trigger later
    // * above is just the name of the parameter
    
    // end animation data

    // exit condition
    private bool _performed;
    public bool performed {get {return _performed;} }
    // end exit condition - used by AttackStateSO to check if it can exit state
    
    // temp variables
    private IEnumerator coroutine;
    // end temp variables


    public AttackState(float damage, float clipLength, String animatorParam)
    {
        this.damage = damage;
        this.clipLength = clipLength;
        this.animatorParam = animatorParam;
    }

    public override void OnEnter(StateMachine stateMachine)
    {

        if(character == null)
        {
            character = stateMachine.GetComponent<Character>();
            animator = character.animator;
            weapon = character.weapon;
        }

        character.attackTrigger = false;    // consume the input

        // base.OnEnter(stateMachine);
        _performed = false;

        // start coRoutine which sets _performed to True at end
        
        coroutine = attackCoroutine();
        stateMachine.StartCoroutine(coroutine); // the method is in MonoBehaviour so it's like this
    }


    private IEnumerator attackCoroutine()
    {
        if(!weapon.setAttackingTrue()) {
            // if weapon is already performing an attack
            _performed = true;  // also set this to true so it can exit transition
            yield break;
            // although this shouldn't happen, but still
            // * this is from previous structure (without StateMachine) - but yeah better safe than sorry
        }
        // TODO - may change above, maybe for button mashing to work
        // * like wait at max a second or 2 to perform the attack

        weapon.damage = damage;
        weapon.clearHits();

        // start animation
        animator.SetBool(animatorParam, true);

        yield return new WaitForSeconds(clipLength);

        animator.SetBool(animatorParam, false);
        weapon.damage = 0f;  // just for safety
        weapon.setAttackingFalse();

        _performed = true;
    }


}