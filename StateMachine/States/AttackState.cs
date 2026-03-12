using System;
using System.Collections;
using UnityEngine;

public class AttackState : MState
{
    // TODO - may add attack cancelling mechanics - or just use shorter animations

    // damage
    private float damage;

    // animation data
    private float clipLength;
    private String animatorParam;    
    // * will use a bool animator param for now - might change it to trigger later
    // * above is just the name of the parameter
    
    // end animation data


    public AttackState(float damage, float clipLength, String animatorParam)
    {
        this.name = "Attack State";
        this.damage = damage;
        this.clipLength = clipLength;
        this.animatorParam = animatorParam;
    }

    protected override void onEnter(StateMachine stateMachine)
    {

        stateMachine.character.attackTrigger = false;    // consume the input

        // base.OnEnter(stateMachine);
        stateMachine.character.attackPerformed = false;

        // start coRoutine which sets _performed to True at end
        stateMachine.StartCoroutine(attackCoroutine(stateMachine.character)); // the method is in MonoBehaviour so it's like this
    }


    private IEnumerator attackCoroutine(Character character)
    {
        Weapon weapon = character.weapon;
        if(!weapon.setAttackingTrue()) {
            // if weapon is already performing an attack
            character.attackPerformed = true;  // also set this to true so it can exit transition
            yield break;
            // although this shouldn't happen, but still
            // * this is from previous structure (without StateMachine) - but yeah better safe than sorry
        }
        // TODO - may change above, maybe for button mashing to work
        // * like wait at max a second or 2 to perform the attack

        weapon.damage = damage;
        weapon.clearHits();

        Animator animator = character.animator;

        // start animation
        animator.SetBool(animatorParam, true);

        yield return new WaitForSeconds(clipLength);

        character.attackTrigger = false;    
        // * consume here again, in case player pressed attack again
        // * result is kinda weird in example like this:
        // *    - user right clicks (attack)
        // *    - animation starts - lenght around 1.8s
        // *    - user presses attack (right click) after 0.4s
        // *    - user doesn't press attack anymore
        // *    - player tries to attack again since the above input wasn't consume
        // *    - user is confused
        // TODO - think about above scenario - maybe set trigger to false if not consumed within 0.5s (or some other interval)

        animator.SetBool(animatorParam, false);
        weapon.damage = 0f;  // just for safety
        weapon.setAttackingFalse();

        character.attackPerformed = true;
    }

    protected override void onUpdate(StateMachine stateMachine){}
    protected override void onExit(StateMachine stateMachine){}


}