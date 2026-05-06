
using UnityEngine;

[CreateAssetMenu(fileName = "KnockbackEvent", menuName = "State Machine/Conditions/NPC knocked back")]
class KnockbackEvent : TCondition
{
    public override bool Check(Entity entity)
    {
        if (entity is not NPCEntity npc)
        {
            return false;
        }
        return npc.knockbackForce != 0f;
    }
}