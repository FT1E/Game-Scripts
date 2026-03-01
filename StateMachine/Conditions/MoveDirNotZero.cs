using UnityEngine;

[CreateAssetMenu(fileName = "MoveDirNotZero", menuName = "Scriptable Objects/State Machine/Conditions/Move Direction Not Zero")]
class MoveDirNotZero : TCondition
{
    public override bool Check(StateMachine stateMachine)
    {
        return stateMachine.GetComponent<Character>().moveDirection != Vector2.zero;
    }
}