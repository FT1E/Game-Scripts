using UnityEngine;

public static class MyPhysics
{
    private readonly static float decellaration = 10f, gravity = -9.81f, gravityMultiplier = 1f, maxGravity = -70f;
    public static void ApplyDragOnVelocityVector(Entity entity)
    {
        // basically just lowers the magnitude of the velocity vector on xz coordinates, and apply gravity on y coordinate
        // should be called every frame when you want to lower the magnitude

        // use arguments to handle below cases
        // but NOT IF this is done by something else, ex. dynamic rigidbody
        // another case when you shouldn't is when you don't want to negate the movement of the entity

        // xz coordinates
        float currentSpeed = GetCurrentSpeed(entity);
        Vector3 velocityVector = entity.velocityVector;

        if (currentSpeed <= 1.5f)
        {
            currentSpeed = 0f;
        }
        else
        {
            // if there is some force on xz coordinates
            currentSpeed -= decellaration * Time.deltaTime;
            
        }
        velocityVector.x = velocityVector.normalized.x * currentSpeed;
        velocityVector.z = velocityVector.normalized.z * currentSpeed;
        
        entity.velocityVector = velocityVector;

    }

    public static void ApplyGravity(Entity entity)
    {
        // y coordinate
        float y = entity.velocityVector.y;
        if (entity.isGrounded)
        {
            y = -1f;
        }
        else if (maxGravity < y)
        {
            y += gravity * gravityMultiplier * Time.deltaTime;
        }
        else
        {
            y = maxGravity;
        }
        entity.velocityVector.y = y;
    }

    public static float GetCurrentSpeed(Entity entity)
    {
        Vector3 velocityVector = entity.velocityVector;
        if(velocityVector.x != 0)
        {
            return Mathf.Abs(velocityVector.x / velocityVector.normalized.x);
        }
        else if(velocityVector.z != 0)
        {
            return Mathf.Abs(velocityVector.z / velocityVector.normalized.z);
        }
        else
        {
            return 0f;
        }
    }
    
}