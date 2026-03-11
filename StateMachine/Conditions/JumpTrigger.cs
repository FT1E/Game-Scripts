using UnityEngine;


[CreateAssetMenu(fileName = "JumpTrigger", menuName = "State Machine/Conditions/Jump Triggered")]
public class JumpTrigger : TCondition
{
    public override bool Check(StateMachine stateMachine)
    {
        return stateMachine.GetComponent<Character>().jumpTrigger;
    }
}
