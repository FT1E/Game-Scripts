
using UnityEngine;

[CreateAssetMenu(fileName = "NPCNonManualMoveFlag", menuName = "State Machine/Conditions/NPC Manual Move Flag == FALSE")]
class NPCNonManualMoveFlag : TCondition
{
    public override bool Check(Entity entity)
    {
        if (entity is not NPCEntity npc)
        {
            return false;
        }
        return !npc.manualMove;
    }
}