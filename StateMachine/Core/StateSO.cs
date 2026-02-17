using UnityEngine;

//[CreateAssetMenu(fileName = "StateSO", menuName = "Scriptable Objects/StateSO")]
// no asset menu for abstract class - will have though for each subclass
public abstract class StateSO : ScriptableObject
{
    // anything implementing needs to specify State, fields and transitions with conditions to other states


    // it can also have fields associated with it
    // example MoveStateSO
    //      - state is of type MoveState
    //      - additional fields - max_speed, acceleration, etc.
    // because those fields can have different values for different users
    // different types of enemies may move with different speeds
    // player will likely move with different speed from enemies
    // but can use this for both - same MoveState, MoveStateSO - just different parameters
    
    
    public readonly State state;

    // transitions to other states
    protected TCondition[] conditions;
    protected StateSO[] transitions;
    // if conditions[i].Check() == true
    // -> transition to state transitions[i]
    // transition to first condition evaluating to true
    // empty condition is also viable

    public StateSO checkTransitions()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            if (conditions[i].Check())
            {
                return transitions[i];
            }
        }
        return null;
    }

}
