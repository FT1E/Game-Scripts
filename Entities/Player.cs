using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player : Entity
{

    [SerializeField]
    private float _maxSpeed;
    public float MaxSpeed { get { return _maxSpeed; } }

    
    [SerializeField]
    private PlayerInfo playerInfo;

    [SerializeField]
    private StateSO initialState;
    private StateMachine stateMachine;

    [SerializeField]
    private InputReader _inputReader = default;
    

    [SerializeField]
    private Transform _cameraTransform;
    public Transform cameraTransform { get { return _cameraTransform; } }

    [SerializeField]
    private Rig _spineRig;
    public Rig spineRig { get { return _spineRig; } }

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
    public Vector2 ForwardDirection {
        get
        {
            float angle = - Mathf.Deg2Rad * cameraTransform.rotation.eulerAngles.y; 
            return new Vector2(
                -Mathf.Sin(angle),
                Mathf.Cos(angle)
            );
        }
    }    
    // end input move direction
    public bool isRunning;
    public bool jumpTrigger;
    public bool attackTrigger;
    public bool lightAttackTrigger;
    // end Input variables

    public bool attackTurn;


    private CharacterController characterController;

    // Input actions system stuff
     private void OnEnable()
    {
        _inputReader.moveEvent += OnMove;
        _inputReader.runStartEvent += RunStart;
        _inputReader.runStopEvent += RunStop;
        _inputReader.jumpStartEvent += JumpStart;
        _inputReader.jumpCancelEvent += JumpCancel;
        _inputReader.attackEvent += AttackTrigger;
        _inputReader.lightAttackEvent += LightAttackTrigger;

        _inputReader.EnableGameplay();



    }

    private void OnDisable()
    {
        _inputReader.moveEvent -= OnMove;
        _inputReader.runStartEvent -= RunStart;
        _inputReader.runStopEvent -= RunStop;
        _inputReader.jumpStartEvent -= JumpStart;
        _inputReader.jumpCancelEvent -= JumpCancel;
        _inputReader.attackEvent -= AttackTrigger;
        _inputReader.lightAttackEvent -= LightAttackTrigger;

        _inputReader.DisableInputSystem();

        
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

    private void LightAttackTrigger()
    {
        lightAttackTrigger = true;
    }
    // end Input actions system stuff

    // managing weapon attack collisions
    

    public override void DisableWeaponCollision(string animatorParam)
    {
        lightAttackTrigger = false;
        attackTrigger = false;      // consume input
        base.DisableWeaponCollision(animatorParam);
    }
    public void SetAnimBoolFalse()
    {
        animator.SetBool("Attack1", false);
    }

    public void EnableTorsoLayer()
    {
        animator.SetLayerWeight(1, 1f);
    }
    public void DisableTorsoLayer()
    {
        // 0.001 instead of 0 so the animation is played and the event which sets the layer weight to 1 is called
        // and small value so it doesn't override the base layer
        animator.SetLayerWeight(1, 0.001f);
    }

    // end weapon attack collisions


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        stateMachine = new StateMachine(initialState);
        weapon.SetHitLayer(7);
        weapon.cancelAttacks = true;
    }

    void Update()
    {
        isGrounded = characterController.isGrounded;
        animator.SetFloat("speed", MyPhysics.GetCurrentSpeed(this) / MaxSpeed);

        MyPhysics.ApplyGravity(this);
        if (MyPhysics.GetCurrentSpeed(this) > MaxSpeed) MyPhysics.ApplyDragOnVelocityVector(this);
        // todo - above isn't really enough, need to do something like maybe if it's not in move state then always apply drag

        stateMachine.Update(this);
        characterController.Move(velocityVector * Time.deltaTime);

        playerInfo.SetPosition(transform.position);
    }

}