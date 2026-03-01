using UnityEngine;

// for now do nothing
// may be used for initial state, and as a bridge connecting 1-action states
// like for example JumpState, just set jump -> idle -> in-airMove (or just stay in idle if grounded)
// basically like a central state connected to (almost) every other state

[CreateAssetMenu(fileName = "MoveStateSO", menuName = "Scriptable Objects/State Machine/States/Idle State")]
public class IdleStateSO : StateSO
{

    public void OnEnable()
    {
        if (_state == null) { 
            _state = new IdleState();
        }
    }
}
