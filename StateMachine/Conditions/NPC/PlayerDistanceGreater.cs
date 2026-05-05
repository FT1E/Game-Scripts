
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDistanceGreater", menuName = "State Machine/Conditions/Farness Distance from player")]
class PlayerDistanceGreater : TCondition
{

    [Tooltip("How far from player should it be, for this condition to be true.")]
    [SerializeField]
    private float distance = 2f;

    [SerializeField]
    private PlayerInfo playerInfo;

    public override bool Check(Entity entity)
    {
        if (entity is not NPCEntity npc)
        {
            return false;
        }
        if (npc.nmAgent.enabled && !npc.nmAgent.isStopped && npc.nmAgent.hasPath)
        {
            return npc.nmAgent.remainingDistance > distance;
        }
        return Vector3.Distance(npc.transform.position, playerInfo.position) > distance;
    }
}