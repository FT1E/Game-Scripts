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
    private Vector3 moveVector;
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
        moveDirection = character.moveDirection;

        CalculateXZMove();
        CalculateYMove();
        RotateCharacter();
        SetAnimatorSpeed();
        character.moveVector = moveVector;

        PerformMove();
    }

    private void PerformMove() {
        characterController.Move(character.moveVector * Time.deltaTime);
    }


    private void CalculateXZMove() {
        if (moveDirection == Vector2.zero)
        {
            currentSpeed = 0f;
            moveVector.x = moveVector.z = 0f;
            return;
        }
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = maxSpeed;
        }


        moveVector.x = moveDirection.x * currentSpeed;
        moveVector.z = moveDirection.y * currentSpeed;
        //moveVector *= currentSpeed;
        // don't wanna affect y with this speed
    }

    private void CalculateYMove() {
        if (characterController.isGrounded) {
            currentGravitySpeed = 0f;
            moveVector.y = -1f;
            return;
        }

        if (currentGravitySpeed > maxGravitySpeed)
        {
            currentGravitySpeed += gravity * gravityMultiplier * Time.deltaTime;
        }
        else {
            currentGravitySpeed = maxGravitySpeed;
        }
        moveVector.y = currentGravitySpeed;
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
        animator.SetFloat("speed", (currentSpeed / maxSpeed) * animatorMaxSpeed);
    }
}
