using UnityEngine;

public class MoveState : MState
{



    // variables for moving on xz coordinates
    private float acceleration;

    // rotation variables
    private float rotationSpeed = 360f;
    public MoveState(float acceleration, float rotationSpeed = 360f)
    {
        name = "Move State";

        this.acceleration = acceleration;
        this.rotationSpeed = rotationSpeed;
        
    }

    protected override void onEnter(Entity entity)
    {
        return;
    }
    protected override void onExit(Entity entity)
    {
        if(entity is not Player p) return;
        if(MyPhysics.GetCurrentSpeed(p) < p.MaxSpeed) SetNewSpeed(p, 0f, Vector2.zero);
        MyPhysics.ApplyDragOnVelocityVector(p);
    }

    protected override void onUpdate(Entity entity)
    {
        if (entity is not Player player) return; 
        if (player.MoveDirection == Vector2.zero) {
            MyPhysics.ApplyDragOnVelocityVector(entity);
            return;
        }
        
        CalculateXZMove(player.isRunning, player.MoveDirection, player);
        RotateCharacter(player.transform, player.MoveDirection);
        // Debug.Log(velocityVector);

    }
    private void CalculateXZMove(bool isRunning, Vector2 moveDirection, Player player) {
        // todo - instant deceleration if player holds CTRL or something

        float currentSpeed = MyPhysics.GetCurrentSpeed(player);
        if (currentSpeed > player.MaxSpeed + 1)
        {
            MyPhysics.ApplyDragOnVelocityVector(player);
            return;
        }
        // else if no force has been applied or if it has been made smaller already just set new speed

        if(!isRunning)
        {
            currentSpeed = player.MaxSpeed * 0.5f;
            
        }// else running
        else if(currentSpeed < player.MaxSpeed)
        {
            // accelerating
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            // running, already max speed
            currentSpeed = player.MaxSpeed;
        }
        SetNewSpeed(player, currentSpeed, moveDirection);
        
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
