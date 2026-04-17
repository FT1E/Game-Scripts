using UnityEngine;

[CreateAssetMenu(fileName = "NavMeshMoveStateSO", menuName = "State Machine/States/NavMesh Move State")]
public class NavMeshMoveStateSO : StateSO
{
    [SerializeField]
    private PlayerInfo playerInfo = default;
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private string specialAnimationParam = "";

    void OnEnable() {
        if (_state == null)
        {
            _state = new NavMeshMoveState(playerInfo, speed, specialAnimationParam);
        }
    }
}