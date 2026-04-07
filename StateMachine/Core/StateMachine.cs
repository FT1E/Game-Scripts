using System;
using UnityEngine;

[Serializable]
public class StateMachine 
{
    // anything using it NEEDS TO SPECIFY initialState
    // probably will make it so that it's an SO (ScriptableObject)
    // the structure of the StateMachine is defined by the transitions that each state has
    // the transitions are stored in the state where they originate from


    
    private StateSO initial = default;

    private StateSO current;
    private StateSO next;

    public StateMachine(StateSO initial)
    {
        this.initial = initial;
        current = initial;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetState(StateSO stateSO)
    {
        current = stateSO;
    }

    // Update is called once per frame
    public void Update(Entity entity)
    {
        next = current.checkTransitions(entity);
        if (next == null)
        {
            // if none found do OnUpdate
            current.state.OnUpdate(entity);
        }
        else {
            // else do OnExit(), transition then OnEnter for new state
            current.state.OnExit(entity);
            current = next;
            next = null;
            current.state.OnEnter(entity);
        }
    }
    // todo - delete this waaay later at the end
    public void PrintStateName()
    {
        Debug.Log(current.state.name);
    }

    public void Reset(Entity entity)
    {
        // * just set state to initial state
        // * used for when cancelling attack animations of mobs
        // todo - if issues arise due to runtime animations, use lock(){}


        current.state.OnExit(entity);
        current = initial;
    }
}
