using System;
using System.Collections;
using UnityEngine;

public class AttackState : MState
{
    // TODO - may add attack cancelling mechanics - or just use shorter animations

    // damage
    private float damage;

    // animation data
    private String animatorParam;    
    // * will use a bool animator param for now - might change it to trigger later
    // * above is just the name of the parameter
    
    // end animation data


    public AttackState(float damage, String animatorParam)
    {
        this.name = "Attack State";
        this.damage = damage;
        this.animatorParam = animatorParam;
    }

    protected override void onEnter(Entity entity)
    {
        // player.attackTrigger = false;    // consume the input
        // * input is consumed later in attack animation event for player
        // enemy doesn't have an input, just tries to attack when it gets close to player

        // base.OnEnter(stateMachine);
        entity.attackPerformed = false;
        entity.weapon.damage = damage;
        entity.animator.SetBool(animatorParam, true);

    }


    protected override void onUpdate(Entity entity){}
    protected override void onExit(Entity entity){}


}