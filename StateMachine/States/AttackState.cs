using UnityEngine;

public class AttackState : MState
{
    // TODO - may add attack cancelling mechanics - or just use shorter animations

    // damage
    private float damage;

    // knockback force
    private float knockbackForce;

    // animation data
    private string animatorParam;    
    // * will use a bool animator param for now - might change it to trigger later
    // * above is just the name of the parameter
    
    // end animation data


    private float rotationSpeed;

    public AttackState(float damage, string animatorParam, float knockbackForce = 0f, float rotationSpeed=360f)
    {
        this.name = "Attack State";
        this.damage = damage;
        this.animatorParam = animatorParam;
        this.knockbackForce = knockbackForce;
        this.rotationSpeed = rotationSpeed;
    }

    protected override void onEnter(Entity entity)
    {
        // * input is consumed later in attack animation event for player
        if(entity is Player p) {
            p.attackPerformed = false;
            p.attackTurn = false;
            return;
        }
        // gradually, but quickly, rotate player in forward direction relative to camera
        
        // base.OnEnter(stateMachine);
        entity.attackPerformed = false;
        entity.weapon.damage = damage;
        entity.weapon.knockbackForce = knockbackForce;
        entity.animator.SetBool(animatorParam, true);

        

    }


    protected override void onUpdate(Entity entity)
    {
        if (entity is not Player p) return;
        if (p.attackTurn) return;

        Vector2 dir = p.ForwardDirection;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.y));
        p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        
        if (Quaternion.Angle(lookRotation, p.transform.rotation) < 1)
        {
            entity.attackPerformed = false;
            entity.weapon.damage = damage;
            entity.weapon.knockbackForce = knockbackForce;
            entity.animator.SetBool(animatorParam, true);
            p.attackTurn = true;
        }

    }
    protected override void onExit(Entity entity)
    {
        // * for safety
        entity.weapon.damage = 0f;
        entity.weapon.knockbackForce = 0f;
    }


}