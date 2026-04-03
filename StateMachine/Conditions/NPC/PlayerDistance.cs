
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDistance", menuName = "State Machine/Conditions/Distance from player")]
class PlayerDistance : TCondition
{

    [Tooltip("How close to player should it be, for this condition to be true.")]
    [SerializeField]
    private float distance = 2f;
    public override bool Check(Entity entity)
    {
        if (entity is not NPCEntity npc)
        {
            return false;
        }
        if (npc.nmAgent.hasPath)
        {
            return npc.nmAgent.remainingDistance <= distance;
        }
        return false;
    }
}