using UnityEngine;

[CreateAssetMenu(fileName = "MoveStateSO", menuName = "State Machine/States/Move State")]
public class MoveStateSO : StateSO
{

    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private float acceleration = 5f;
    [SerializeField]
    private float deceleration = 10f;

    [Tooltip("Animator max speed is for blend tree parameter, made of walking/running animation clips")]
    [SerializeField]
    private float animatorMaxSpeed = 2f;

    [Tooltip("Rotation speed is in degrees per second")]
    [SerializeField]
    private float rotationSpeed = 360f;

    public void OnEnable()
    {
        if (_state == null)
        {
            _state = new MoveState(maxSpeed, acceleration, deceleration, rotationSpeed, animatorMaxSpeed);
        }
    }
}
