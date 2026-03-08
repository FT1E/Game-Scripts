using UnityEngine;

public class MoveState : MState
{
    // todo - may add decelleration for XZ move

    // components used
    private Character character;
    private Animator animator;
    private CharacterController characterController;

    // variables for moving on xz coordinates
    private float currentSpeed = 0f;
    private float maxSpeed, acceleration;

    // variables for moving on y coordinate
    private float gravity = -9.81f;
    private float gravityMultiplier = 1f;
    private float maxGravitySpeed = -70f;
    private float currentGravitySpeed = 0f;

    // rotation variables
    private float rotationSpeed = 360f;


    // animator variables
    private float animatorMaxSpeed = 2f;

    // temp variables
    private Vector2 moveDirection;
    private Vector3 velocityVector;
    private Vector3 rotationVector;

    public MoveState(float maxSpeed, float acceleration, float rotationSpeed = 360f, float animatorMaxSpeed = 2f)
    {
        name = "Move State";

        this.maxSpeed = maxSpeed;
        this.acceleration = acceleration;
        this.rotationSpeed = rotationSpeed;
        this.animatorMaxSpeed = animatorMaxSpeed;
    }

    public override void OnEnter(StateMachine stateMachine)
    {
        // todo - might change this - for some states where I might want to keep a momentum, idk
        currentSpeed = 0f;

        if (character == null) {
            character = stateMachine.GetComponent<Character>();
            animator = character.animator;
            characterController = character.characterController;
            // for rotation use character.transform.rotation
        }
    }
    public override void OnExit(StateMachine stateMachine)
    {
        currentSpeed = 0f;
        animator.SetFloat("speed", 0f);
    }

    public override void OnUpdate(StateMachine stateMachine)
    {
        velocityVector = character.velocityVector;
        moveDirection = character.moveDirection;
        
        CalculateXZMove();
        RotateCharacter();
        SetAnimatorSpeed();
        CalculateYMove();

        character.velocityVector = velocityVector;
        PerformMove();

        Debug.Log(velocityVector.y);

    }

    private void PerformMove() {
        characterController.Move(character.velocityVector * Time.deltaTime);
    }


    private void CalculateXZMove() {
        // ! TODO - implement decelaration
        // * - not holding shift - decelerate to 0
        // * - if run previously with shift hold - decelarate slower
        // * - also maybe instant deceleration if player holds CTRL or something

        

        // ! only accelerate if player holds shift
        // if (currentSpeed < maxSpeed)
        // {
        //     currentSpeed += acceleration * Time.deltaTime;
        // }
        // else
        // {
        //     currentSpeed = maxSpeed;
        // }

        currentSpeed = 0.75f * maxSpeed;

        velocityVector.x = moveDirection.x * currentSpeed;
        velocityVector.z = moveDirection.y * currentSpeed;
        //moveVector *= currentSpeed;
        // don't wanna affect y with this speed
    }

    private void CalculateYMove() {
        if (characterController.isGrounded && velocityVector.y <= 0) {
            currentGravitySpeed = 0f;
            velocityVector.y = -1f;
            return;
        }

        if (velocityVector.y > maxGravitySpeed)
        {
            velocityVector.y += gravity * gravityMultiplier * Time.deltaTime;
        }
        else {
            velocityVector.y = maxGravitySpeed;
        }
        // velocityVector.y *= Time.deltaTime;
    }

    private void RotateCharacter() {
        if (moveDirection != Vector2.zero)
        {

            // rotate the character toward 
            rotationVector.x = moveDirection.x;
            rotationVector.z = moveDirection.y;
            Quaternion lookRotation = Quaternion.LookRotation(rotationVector);
            character.transform.rotation = Quaternion.RotateTowards(character.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void SetAnimatorSpeed()
    {
        animator.SetFloat("speed", (Mathf.Abs(velocityVector.x)  + Mathf.Abs(velocityVector.z) > 0 ? animatorMaxSpeed * 1.5f : 0f) );
        // ! todo - uncomment below after
        // animator.SetFloat("speed", (currentSpeed / maxSpeed) * animatorMaxSpeed);
    }
}
