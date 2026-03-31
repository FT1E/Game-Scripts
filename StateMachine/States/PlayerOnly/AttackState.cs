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

    protected override void onEnter(Entity entity)
    {
        if(entity.GetPlayer() is not Player player) return;

        // player.attackTrigger = false;    // consume the input
        // * input is consumed later in player

        // base.OnEnter(stateMachine);
        entity.attackPerformed = false;
        player.weapon.damage = damage;
        player.animator.SetBool(animatorParam, true);

    }


    protected override void onUpdate(Entity entity)
    {
        
    }
    protected override void onExit(Entity entity){}


}