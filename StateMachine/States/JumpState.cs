using System.Collections;
using UnityEngine;

public class JumpState : MState
{

    private float jumpPower;
    
    public JumpState(float jumpPower)
    {
        this.name = "Jump state";
        this.jumpPower = jumpPower;
    }

    protected override void onEnter(Entity entity)
    {
        // apply jump push - this will slowly get smaller in MoveState, where gravity is applied
        entity.velocityVector.y += jumpPower;
        
        // consume jump trigger input
        if (entity.GetPlayer() is Player p)
        {
            p.jumpTrigger = false;
        }

        // todo - trigger jump animation

    }
    protected override void onUpdate(Entity entity){}
    protected override void onExit(Entity entity){}

}