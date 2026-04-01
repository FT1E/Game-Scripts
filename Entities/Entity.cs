using UnityEngine;

public class Entity : MonoBehaviour
{
    // stuff every entity should have
    public Vector3 velocityVector;  // for moving/applying forces to entity from scripts
    // ! if there are possible race conditions on velocityVector, consider how to implement safe locking s.t everything is applied correctly


    [SerializeField]
    protected float _health;
    public float Health { get {return _health; }}

    protected bool _isGrounded = true;
    public bool isGrounded { get {return _isGrounded; }}
    

    public bool attackPerformed = false;
}
