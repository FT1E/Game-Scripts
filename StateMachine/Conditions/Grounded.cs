
using UnityEngine;

[CreateAssetMenu(fileName = "Grounded", menuName = "Scriptable Objects/State Machine/Conditions/Character Grounded")]
class Grounded : TCondition
{
    public override bool Check(StateMachine stateMachine)
    {
        return stateMachine.GetComponent<Character>().characterController.isGrounded;
    }
}