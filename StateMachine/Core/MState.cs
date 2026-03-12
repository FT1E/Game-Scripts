using UnityEngine;

public abstract class MState
{
    public string name = "State class";
    
    private Object enterLock = new Object();
    private Object updateLock = new Object();
    private Object exitLock = new Object();


    // passing the StateMachine using as argument to get any components if needed
    public void OnEnter(StateMachine stateMachine) {
        // Debug.Log($"state <{name}> - on enter");
        lock (enterLock)
        {
            onEnter(stateMachine);
        }
    }

    public void OnUpdate(StateMachine stateMachine)
    {
        // Debug.Log($"state <{name}> - on update");
        lock (updateLock)
        {
            onUpdate(stateMachine);
        }
    }

    public void OnExit(StateMachine stateMachine)
    {
        // Debug.Log($"state <{name}> - on exit");
        lock(exitLock)
        {
            onExit(stateMachine);
        }
    }

    protected abstract void onEnter(StateMachine stateMachine);
    protected abstract void onUpdate(StateMachine stateMachine);
    protected abstract void onExit(StateMachine stateMachine);

}
