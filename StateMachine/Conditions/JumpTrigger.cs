using UnityEngine;


[CreateAssetMenu(fileName = "InAir", menuName = "Scriptable Objects/State Machine/Conditions/Jump Triggered")]
public class JumpTrigger : TCondition
{
    public override bool Check(StateMachine stateMachine)
    {
        return stateMachine.GetComponent<Character>().jumpTrigger;
    }
}
