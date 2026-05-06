using UnityEngine;

[CreateAssetMenu(fileName = "JumpStateSO", menuName = "State Machine/States/Jump State")]
public class JumpStateSO : StateSO
{

    [SerializeField]
    private float jumpPower = 5f;

    public void OnEnable()
    {
        if (_state == null)
        {
            _state = new JumpState(jumpPower);
        }
    }
}
