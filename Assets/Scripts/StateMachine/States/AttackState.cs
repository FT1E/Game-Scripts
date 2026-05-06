using UnityEngine;

public class AttackState : MState
{
    // TODO - may add attack cancelling mechanics for player - or just use shorter animations

    // damage
    private float damage;

    // knockback force
    private float knockbackForce;

    // animation data
    private string animatorParam;    
    // * will use a bool animator param for now - might change it to trigger later
    // * above is just the name of the parameter
    
    // end animation data

    // * how fast does the player's character rotate forward in the camera's POV - degrees per second, should be pretty high so attack isn't delayed for much
    private float rotationSpeed;

    // * whether the character can move during attack or not
    // ? if this is allowed, then player won't rotate, it will just attack in the direction he is facing
    private bool moveAllowed;

    public AttackState(float damage, string animatorParam, float knockbackForce = 0f, float rotationSpeed=360f, bool moveAllowed = false)
    {
        this.name = "Attack State";
        this.damage = damage;
        this.animatorParam = animatorParam;
        this.knockbackForce = knockbackForce;
        this.rotationSpeed = rotationSpeed;
        this.moveAllowed = moveAllowed;
    }

    protected override void onEnter(Entity entity)
    {
        entity.timeSinceLastAtk = 0f;
        // * special handling for player - if the attack can be performed while moving or not
        if(entity is Player p) {
            if (moveAllowed)
            {
                p.attackPerformed = true;
                p.attackTurn = true;
                p.lightAttackTrigger = false;
                setValues(entity, damage, knockbackForce, animatorParam);
            } else
            {
                // * input is consumed later in attack animation event for player
                p.attackPerformed = false;
                p.attackTurn = false;
                p.DisableTorsoLayer();
                p.animator.SetTrigger("CancelLightAttack");
            }
            return;
        }
        // gradually, but quickly, rotate player in forward direction relative to camera
        
        // base.OnEnter(stateMachine);
        entity.attackPerformed = false;
        setValues(entity, damage, knockbackForce, animatorParam);
    }
    

    protected override void onUpdate(Entity entity)
    {
        if (entity is not Player p) return;
        if (p.attackTurn) return;
        // todo - for player add rotation around x axis

        Vector2 dir = p.ForwardDirection;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.y));
        p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        p.spineRig.weight = Mathf.MoveTowards(p.spineRig.weight, 1f, rotationSpeed * Time.deltaTime / 360f);
        
        if (Quaternion.Angle(lookRotation, p.transform.rotation) < 1)
        {
            p.spineRig.weight = 1f;
            entity.attackPerformed = false;
            setValues(entity, damage, knockbackForce, animatorParam);
            p.attackTurn = true;
        }

    }
    protected override void onExit(Entity entity)
    {
        // * for safety
        // * also when attack is cancelled so these are set properly
        entity.animator.SetBool(animatorParam, false);
        entity.timeSinceLastAtk = 0f;
        if(entity is Player p) p.spineRig.weight = 0f;
    }
    
    private void setValues(Entity entity, float damage, float knockbackForce, string animatorParam)
    {
        entity.weapon.SetDamage(damage);
        // ! below is commented out - in case later I want to switch it back to setting knockbacks per attack instead of manually from the entity
        // entity.weapon.SetKnockback(knockbackForce);
        entity.animator.SetBool(animatorParam, true);
    }


}