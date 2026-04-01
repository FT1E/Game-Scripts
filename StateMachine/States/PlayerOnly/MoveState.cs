using System.Net;
using UnityEngine;

public class MoveState : MState
{

    // todo - don't deal with components here


    // variables for moving on xz coordinates
    private float maxSpeed, acceleration;
    private readonly float jogSpeed;

    // rotation variables
    private float rotationSpeed = 360f;
    public MoveState(float maxSpeed, float acceleration, float rotationSpeed = 360f)
    {
        name = "Move State";

        this.maxSpeed = maxSpeed;
        this.acceleration = acceleration;
        this.rotationSpeed = rotationSpeed;
        this.jogSpeed = 0.5f * maxSpeed;
        
    }

    protected override void onEnter(Entity entity)
    {
        return;
    }
    protected override void onExit(Entity entity)
    {
        MyPhysics.ApplyDragOnVelocityVector(entity);
    }

    protected override void onUpdate(Entity entity)
    {
        if (entity is not Player player) return; 
        if (player.MoveDirection == Vector2.zero) return;
        
        CalculateXZMove(player.isRunning, player.MoveDirection, entity);
        RotateCharacter(player.transform, player.MoveDirection);
        // Debug.Log(velocityVector);

    }
    private void CalculateXZMove(bool isRunning, Vector2 moveDirection, Entity entity) {
        // todo - instant deceleration if player holds CTRL or something

        float currentSpeed = MyPhysics.GetCurrentSpeed(entity);
        if (currentSpeed > maxSpeed + 1)
        {
            MyPhysics.ApplyDragOnVelocityVector(entity);
            return;
        }
        // else if no force has been applied or if it has been made smaller already just set new speed

        if(!isRunning)
        {
            currentSpeed = jogSpeed;
            
        }// else running
        else if(currentSpeed < maxSpeed)
        {
            // accelerating
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            // running, already max speed
            currentSpeed = maxSpeed;
        }
        SetNewSpeed(entity, currentSpeed, moveDirection);
        
    }

    private void SetNewSpeed(Entity entity, float speed, Vector2 moveDirection)
    {
        entity.velocityVector.x = moveDirection.x * speed;
        entity.velocityVector.z = moveDirection.y * speed;
    }



    private void RotateCharacter(Transform transform, Vector2 moveDirection) {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.y));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

}
