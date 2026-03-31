using UnityEngine;

public abstract class MState
{
    public string name = "State class";


    // passing the StateMachine using as argument to get any components if needed
    public virtual void OnEnter(Entity entity) {
        // Debug.Log($"state <{name}> - on enter");
        onEnter(entity);
    }

    public virtual void OnUpdate(Entity entity)
    {
        onUpdate(entity);
    }

    public virtual void OnExit(Entity entity)
    {
        onExit(entity);
    }

    protected abstract void onEnter(Entity entity);
    protected abstract void onUpdate(Entity entity);
    protected abstract void onExit(Entity entity);

}
