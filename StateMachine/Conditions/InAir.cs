using UnityEngine;

[CreateAssetMenu(fileName = "InAir", menuName = "Scriptable Objects/State Machine/Conditions/Character In Air (Not grounded)")]
class InAir : TCondition
{
    public override bool Check(StateMachine stateMachine)
    {
        return ! stateMachine.GetComponent<Character>().characterController.isGrounded;
    }
}