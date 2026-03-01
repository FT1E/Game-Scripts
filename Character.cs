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

    

    protected Vector2 _moveDirection;
    public Vector2 moveDirection { get { return _moveDirection; } }


    public Vector3 moveVector;
    public bool jumpTrigger;
    public bool isRunning;
    public bool attackTrigger;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
