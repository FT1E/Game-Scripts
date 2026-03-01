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


    protected MState _state;
    public MState state { get { return _state; } }

    // transitions to other states
    [SerializeField] private Transition[] transitions;
    public StateSO checkTransitions(StateMachine stateMachine)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            if (transitions[i].condition.Check(stateMachine))
            {
                return transitions[i].toState;
            }
        }
        return null;
    }

}
