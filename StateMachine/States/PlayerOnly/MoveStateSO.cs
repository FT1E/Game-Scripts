using UnityEngine;

[CreateAssetMenu(fileName = "MoveStateSO", menuName = "State Machine/States/Move State")]
public class MoveStateSO : StateSO
{

    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private float acceleration = 5f;

    [Tooltip("Rotation speed is in degrees per second")]
    [SerializeField]
    private float rotationSpeed = 360f;

    public void OnEnable()
    {
        if (_state == null)
        {
            _state = new MoveState(maxSpeed, acceleration, rotationSpeed);
        }
    }
}
