using UnityEngine;


[CreateAssetMenu(fileName = "AttackTrigger", menuName = "Scriptable Objects/State Machine/Conditions/Attack Triggered")]
public class AttackTrigger : TCondition
{
    public override bool Check(StateMachine stateMachine)
    {
        return stateMachine.GetComponent<Character>().attackTrigger;
    }
}
