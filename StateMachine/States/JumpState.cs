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

    protected override void onEnter(StateMachine stateMachine)
    {
        //base.OnEnter(stateMachine);

        Character character = stateMachine.character;

        // apply jump push - this will slowly get smaller in MoveState, where gravity is applied
        character.velocityVector.y += jumpPower;
        
        // consume jump trigger input
        character.jumpTrigger = false;  

        // todo - trigger jump animation

    }
    protected override void onUpdate(StateMachine stateMachine){}
    protected override void onExit(StateMachine stateMachine){}

}