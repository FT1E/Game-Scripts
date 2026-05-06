
using UnityEngine;

[CreateAssetMenu(fileName = "NPCManualMoveFlag", menuName = "State Machine/Conditions/NPC Manual Move Flag == TRUE")]
class NPCManualMoveFlag : TCondition
{
    public override bool Check(Entity entity)
    {
        if (entity is not NPCEntity npc)
        {
            return false;
        }
        return npc.manualMove;
    }
}