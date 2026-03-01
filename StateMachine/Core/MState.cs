using UnityEngine;

public abstract class MState
{
    public string name = "State class";

    // passing the StateMachine using as argument to get any components if needed
    public virtual void OnEnter(StateMachine stateMachine) {
        Debug.Log($"state <{name}> - on enter");
    }

    public virtual void OnUpdate(StateMachine stateMachine)
    {
        Debug.Log($"state <{name}> - on update");
    }

    public virtual void OnExit(StateMachine stateMachine)
    {
        Debug.Log($"state <{name}> - on exit");
    }
}
