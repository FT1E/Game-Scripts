
using UnityEngine;

[CreateAssetMenu(fileName = "EmptyCondition", menuName = "Scriptable Objects/State Machine/Conditions/Empty Condition")]

class EmptyCondition : TCondition {

    public override bool Check(StateMachine stateMachine) {
        return true;
    }
}