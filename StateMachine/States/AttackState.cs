using System;
using System.Collections;
using UnityEngine;

public class AttackState : MState
{
    // TODO - may add attack cancelling mechanics - or just use shorter animations

    // damage
    private float damage;

    // knockback force
    private float knockbackForce;

    // animation data
    private String animatorParam;    
    // * will use a bool animator param for now - might change it to trigger later
    // * above is just the name of the parameter
    
    // end animation data


    public AttackState(float damage, String animatorParam, float knockbackForce = 0f)
    {
        this.name = "Attack State";
        this.damage = damage;
        this.animatorParam = animatorParam;
        this.knockbackForce = knockbackForce;
    }

    protected override void onEnter(Entity entity)
    {
        // player.attackTrigger = false;    // consume the input
        // * input is consumed later in attack animation event for player
        // enemy doesn't have an input, just tries to attack when it gets close to player

        // base.OnEnter(stateMachine);
        entity.attackPerformed = false;
        entity.weapon.damage = damage;
        entity.weapon.knockbackForce = knockbackForce;
        entity.animator.SetBool(animatorParam, true);

        // rotate the player character according to camera POV
        if(entity is Player p)
        {
            // todo - maybe an intermediate state where player rotates gradually not instantly
            // todo - but can't be too long (like it should be a sceond at most, maybe half a second)
            Vector2 direction = p.ForwardDirection;
            p.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.y));
        }

    }


    protected override void onUpdate(Entity entity){}
    protected override void onExit(Entity entity)
    {
        // * for safety
        entity.weapon.damage = 0f;
        entity.weapon.knockbackForce = 0f;
    }


}