using System;
using UnityEngine;

public class MoveState : MState
{
    // todo - maybe move stuff - so that physics is done in Character, and this is for applying / or trying to move the character

    // variables for moving on xz coordinates
    private float maxSpeed, acceleration, decelaration;

    // variables for moving on y coordinate
    private readonly float gravity = -9.81f;
    private float gravityMultiplier = 1f;   // can be passed as argument
    private float maxGravitySpeed = -70f;   // can be passed as argument

    // rotation variables
    private float rotationSpeed = 360f;


    // animator variables
    private float animatorMaxSpeed = 2f;

    // temp variables
    private Vector2 moveDirection;
    private Vector3 velocityVector;
    private Vector3 rotationVector;
    private float tempMaxSpeed, xDir, zDir;
    private float temp;

    public MoveState(float maxSpeed, float acceleration, float decelaration, float rotationSpeed = 360f, float animatorMaxSpeed = 2f)
    {
        name = "Move State";

        this.maxSpeed = maxSpeed;
        this.acceleration = acceleration;
        this.decelaration = decelaration;
        this.rotationSpeed = rotationSpeed;
        this.animatorMaxSpeed = animatorMaxSpeed;
    }

    protected override void onEnter(StateMachine stateMachine)
    {
        return;
    }
    protected override void onExit(StateMachine stateMachine)
    {
        stateMachine.character.animator.SetFloat("speed", 0f);
    }

    protected override void onUpdate(StateMachine stateMachine)
    {
        Character character = stateMachine.character;
        velocityVector = character.velocityVector;
        moveDirection = character.moveDirection;
        
        CalculateXZMove(character.isRunning);
        RotateCharacter(character.transform);
        SetAnimatorSpeed();
        CalculateYMove(character.characterController.isGrounded);

        stateMachine.character.velocityVector = velocityVector;
        PerformMove(stateMachine);

        // Debug.Log(velocityVector);

    }

    private void PerformMove(StateMachine stateMachine) {
        stateMachine.character.characterController.Move(velocityVector * Time.deltaTime);
    }


    private void CalculateXZMove(bool isRunning) {
        // todo - instant deceleration if player holds CTRL or something

        float currentSpeed;

        if(velocityVector.x != 0)
        {
            currentSpeed = Mathf.Abs(velocityVector.x / velocityVector.normalized.x);
        }
        else if(velocityVector.z != 0)
        {
            currentSpeed = Mathf.Abs(velocityVector.z / velocityVector.normalized.z);
        }
        else
        {
            currentSpeed = 0f;
        }


        if(moveDirection != Vector2.zero) tempMaxSpeed = (isRunning ? maxSpeed : 0.5f * maxSpeed);
        else tempMaxSpeed = 0f;


        // if a force has been applied so currentSpeed is greater than the above limit
        if(currentSpeed > tempMaxSpeed)
        {
            if(currentSpeed < 0.5f * maxSpeed)
            {
                // tempMaxSpeed == 0 is implied, since tempMS < currentSpeed < 0.5f * mSpeed
                currentSpeed = 0f;
            }
            else 
            {
                // decelerate
                currentSpeed -= decelaration * Time.deltaTime;
                // keep same directions
                xDir = velocityVector.normalized.x;
                zDir = velocityVector.normalized.z;    
            }
        }
        else
        {
            if(tempMaxSpeed == 0.5f * maxSpeed)
            {
                currentSpeed = 0.5f * maxSpeed;
            }
            else
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
            xDir = moveDirection.x;
            zDir = moveDirection.y;
        }
        
        velocityVector.x = xDir * currentSpeed;
        velocityVector.z = zDir * currentSpeed;
        // don't wanna affect y with this speed

    }

    private void CalculateYMove(bool isGrounded) {
        if (isGrounded && velocityVector.y <= 0) {
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

    private void RotateCharacter(Transform transform) {
        if (moveDirection != Vector2.zero)
        {

            // rotate the character toward 
            rotationVector.x = moveDirection.x;
            rotationVector.z = moveDirection.y;
            Quaternion lookRotation = Quaternion.LookRotation(rotationVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void SetAnimatorSpeed()
    {
        // animator.SetFloat("speed", (Mathf.Abs(velocityVector.x)  + Mathf.Abs(velocityVector.z) > 0 ? animatorMaxSpeed * 1.5f : 0f) );
        // ! todo - uncomment after
        // animator.SetFloat("speed", (currentSpeed / maxSpeed) * animatorMaxSpeed);
    }
}
