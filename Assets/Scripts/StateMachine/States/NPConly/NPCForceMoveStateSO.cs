using UnityEngine;

[CreateAssetMenu(fileName = "NPCForceMoveStateSO", menuName = "State Machine/States/NPC Force Move State")]
public class NPCForceMoveStateSO : StateSO
{
    [SerializeField]
    private PlayerInfo playerInfo = default;


    void OnEnable() {
        if (_state == null)
        {
            _state = new NPCForceMoveState(playerInfo);
        }
    }
}