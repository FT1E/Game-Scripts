using UnityEngine;

public abstract class State
{
    // passing the StateMachine using as argument to get any components if needed
    public virtual void OnEnter(StateMachine stateMachine) {
        Debug.Log("state - on enter");
    }

    public virtual void OnUpdate(StateMachine stateMachine)
    {
        Debug.Log("state - on update");
    }

    public virtual void OnExit(StateMachine stateMachine)
    {
        Debug.Log("state - on exit");
    }
}
