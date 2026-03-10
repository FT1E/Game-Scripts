using System.Collections;
using UnityEngine;

public class JumpState : MState
{

    private float jumpPower;
    private Character character = null;
    
    public JumpState(float jumpPower)
    {
        this.name = "Jump state";
        this.jumpPower = jumpPower;
    }

    public override void OnEnter(StateMachine stateMachine)
    {
        //base.OnEnter(stateMachine);
        if (character == null)
        {
            character = stateMachine.GetComponent<Character>();
        }

        // apply jump push - this will slowly get smaller in MoveState, where gravity is applied
        character.velocityVector.y += jumpPower;
        
        // consume jump trigger input
        character.jumpTrigger = false;  

        // todo - trigger jump animation

    }
}