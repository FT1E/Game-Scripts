using UnityEngine;

// a condition that needs to be fulfilled for a transition to be possible
// in State - transitions should be ordered by priority
// as the first TCondition that evaluates to true will be the trigger to that new State
public abstract class TCondition : ScriptableObject
{

    public abstract bool Check(StateMachine stateMachine);  
    // argument passed for getting runtime info, like 
    //  - characterController.IsGrounded
    //  - input checking - or rather the variables which change depending on input (like player move_direction, jump_trigger, attack_trigger, etc.)
}
