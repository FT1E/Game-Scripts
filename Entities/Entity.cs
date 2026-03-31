using UnityEngine;

public class Entity
{
    // stuff every entity should have
    public Vector3 velocityVector;  // for moving/applying forces to entity from scripts
    // ! if there are possible race conditions on velocityVector, consider how to implement safe locking s.t everything is applied correctly


    [SerializeField]
    protected float _health;
    public float Health { get {return _health; }}


    // for when conditions need to get input state/trigger info
    // and for player-only states which manipulate on stuff which only player has
    // if it's null then they return false/do nothing
    private Player player = null;
    public virtual Player GetPlayer(){ return player; }
    public void SetPlayer(Player player){ this.player = player; }


    private bool isGrounded = true;
    public void SetGrounded(bool val)
    {
        isGrounded = val;
    }
    public bool Grounded(){ return isGrounded; }

    public bool attackPerformed = false;



    private readonly float decellaration = 10f, gravity = -9.81f, gravityMultiplier = 1f, maxGravity = -70f;
    public void ApplyDragOnVelocityVector()
    {
        // basically just lowers the magnitude of the velocity vector on xz coordinates, and apply gravity on y coordinate
        // should be called every frame when you want to lower the magnitude

        // use arguments to handle below cases
        // but NOT IF this is done by something else, ex. dynamic rigidbody
        // another case when you shouldn't is when you don't want to negate the movement of the entity

        // xz coordinates
        float currentSpeed =GetCurrentSpeed();

        if (currentSpeed < 1f)
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

    }

    public void ApplyGravity()
    {
        // y coordinate
        if (isGrounded)
        {
            velocityVector.y = -1f;
        }
        else if (maxGravity < velocityVector.y)
        {
            velocityVector.y += gravity * gravityMultiplier * Time.deltaTime;
        }
        else
        {
            velocityVector.y = maxGravity;
        }
    }

    public float GetCurrentSpeed()
    {
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
