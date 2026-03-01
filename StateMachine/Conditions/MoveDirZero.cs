using UnityEngine;

[CreateAssetMenu(fileName = "MoveDirZero", menuName = "Scriptable Objects/State Machine/Conditions/Move Direction == Zero")]
class MoveDirZero : TCondition
{
    public override bool Check(StateMachine stateMachine)
    {
        return stateMachine.GetComponent<Character>().moveDirection == Vector2.zero;
    }
}