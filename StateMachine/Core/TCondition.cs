using UnityEngine;

// a condition that needs to be fulfilled for a transition to be possible
// in State - transitions should be ordered by priority
// as the first TCondition that evaluates to true will be the trigger to that new State
public abstract class TCondition
{

    public abstract bool Check();
}
