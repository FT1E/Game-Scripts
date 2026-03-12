using UnityEngine;

public class StateMachine : MonoBehaviour
{
    // anything using it NEEDS TO SPECIFY initialState
    // probably will make it so that it's an SO (ScriptableObject)
    // the structure of the StateMachine is defined by the transitions that each state has
    // the transitions are stored in the state where they originate from


    [Tooltip("You need to specify initial state")]
    [SerializeField] 
    private StateSO initial = default;

    private StateSO current;
    private StateSO next;

    private Character _character;
    public Character character { get { return _character; } }

    private void Awake() {
        _character = GetComponent<Character>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current = initial;
        current.state.OnEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        // todo bug fix - set up locks for getting each state, acquire lock at start of update, and release it at end
        // todo - also state onEnter, onUpdate, onExit - at beginning they get components from this/stateMachine argument
        // todo - lock acquire/release is done in stateSO

        // Debug.Log($"State name : {current.state.name}");
        // transitions should be ordered by priority
        // check for transitions 
        next = current.checkTransitions(this);
        if (next == null)
        {
            // if none found do OnUpdate
            current.state.OnUpdate(this);
        }
        else {
            // else do OnExit(), transition then OnEnter for new state
            current.state.OnExit(this);
            current = next;
            next = null;
            current.state.OnEnter(this);
        }

    }
}
