using UnityEngine;

public class Character : MonoBehaviour
{
    // Usage for now:
    //  - so that states in a StateMachine get whatever info they need

    [SerializeField]
    protected CharacterController _characterController;
    public CharacterController characterController { get { return _characterController; } }


    [SerializeField]
    protected Animator _animator;
    public Animator animator { get { return _animator; } }

    
    [SerializeField]
    protected Weapon _weapon;
    public Weapon weapon { get { return _weapon; } }

    protected Vector2 _moveDirection;
    virtual public Vector2 moveDirection { get { return _moveDirection; } }


    public Vector3 velocityVector;
    public bool jumpTrigger;
    public bool isRunning;
    public bool attackTrigger;

    public bool attackPerformed = false;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
}
