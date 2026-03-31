using Unity.VisualScripting;
using UnityEngine;
using System;


[Serializable]
public class Transition
{
    [SerializeField]
    private StateSO _toState;
    public StateSO toState { get {return _toState;} }

    [SerializeField]
    private TCondition[] conditions;

    public bool CheckConditions(Entity entity)
    {
        // if condition empty it skips loop and returns true
        foreach (TCondition condition in conditions)
        {
            if(!condition.Check(entity)) return false;
        }
        return true;
    }

}
