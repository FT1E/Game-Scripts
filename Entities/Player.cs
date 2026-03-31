using UnityEngine;

public class Player : MonoBehaviour
{

    private Entity playerEntity; 

    // todo - add state machine after you modify it so it's non-MonoBehaviour
    //  * need to work out how to do transitions in the modified version
    [SerializeField]
    private StateSO initialState;
    private StateMachine stateMachine;

    [SerializeField]
    private InputReader _inputReader = default;
    

    [SerializeField]
    private Transform cameraTransform;

    // Input variables

    // input move direction
    private Vector2 _moveDirection;
    // below get method for getting the relative to camera move direction
    public Vector2 MoveDirection {
        get
        {
            float angle = - Mathf.Deg2Rad * cameraTransform.rotation.eulerAngles.y; 
            return new Vector2(
                _moveDirection.x * Mathf.Cos(angle) - _moveDirection.y * Mathf.Sin(angle),
                _moveDirection.x * Mathf.Sin(angle) + _moveDirection.y * Mathf.Cos(angle)
            );
        }
    }    
    // end input move direction
    public bool isRunning;
    public bool jumpTrigger;
    public bool attackTrigger;
    
    // end Input variables


    // Weapon script variable
    [SerializeField]
    private Weapon _weapon = default;
    public Weapon weapon { get{return _weapon; }}
    [SerializeField]
    private AttackAnimationEvent attackAnimation;

    private CharacterController characterController;
    [SerializeField] private Animator _animator;
    public Animator animator { get {return _animator;}}

    // Input actions system stuff
     private void OnEnable()
    {
        _inputReader.moveEvent += OnMove;
        _inputReader.runStartEvent += RunStart;
        _inputReader.runStopEvent += RunStop;
        _inputReader.jumpStartEvent += JumpStart;
        _inputReader.jumpCancelEvent += JumpCancel;
        _inputReader.attackEvent += AttackTrigger;

        _inputReader.EnableGameplay();


        attackAnimation.WindUpEnd += EnableWeaponCollision;
        attackAnimation.WindDownStart += DisableWeaponCollision;
        attackAnimation.End += SetAnimBoolFalse;

    }

    private void OnDisable()
    {
        _inputReader.moveEvent -= OnMove;
        _inputReader.runStartEvent -= RunStart;
        _inputReader.runStopEvent -= RunStop;
        _inputReader.jumpStartEvent -= JumpStart;
        _inputReader.jumpCancelEvent -= JumpCancel;
        _inputReader.attackEvent -= AttackTrigger;

        _inputReader.DisableInputSystem();

        
        attackAnimation.WindUpEnd -= EnableWeaponCollision;
        attackAnimation.WindDownStart -= DisableWeaponCollision;
        attackAnimation.End -= SetAnimBoolFalse;
    }

    // below methods attached to unity input actions system
    private void OnMove(Vector2 input)
    {
        _moveDirection = input;
    }
    private void RunStart()
    {
        isRunning = true;
    }

    private void RunStop()
    {
        isRunning = false;
    }

    private void JumpStart()
    {
        jumpTrigger = true;
    }

    private void JumpCancel()
    {
        jumpTrigger = false;
    }

    private void AttackTrigger() {
        attackTrigger = true;
    }
    // end Input actions system stuff

    // managing weapon attack collisions
    public void EnableWeaponCollision()
    {
        Debug.Log("Weapon collision enabled");
        weapon.setAttackingTrue();
    }

    public void DisableWeaponCollision()
    {
        playerEntity.attackPerformed = true;
        attackTrigger = false;      // consume input
        animator.SetBool("Attack1", false);
        Debug.Log("Weapon collision disabled");
        weapon.setAttackingFalse();
    }
    public void SetAnimBoolFalse()
    {
        animator.SetBool("Attack1", false);
    }
    // end weapon attack collisions


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerEntity = new Entity();
        playerEntity.SetPlayer(this);
        stateMachine = new StateMachine(initialState);
    }

    void Update()
    {
        playerEntity.SetGrounded(characterController.isGrounded);

        playerEntity.ApplyGravity();
        if (MoveDirection == Vector2.zero) playerEntity.ApplyDragOnVelocityVector();

        // todo - a variable for when to apply drag on XZ, so it doesn't negate the player controlled movement
        stateMachine.Update(playerEntity);
        characterController.Move(playerEntity.velocityVector * Time.deltaTime);
    }

}