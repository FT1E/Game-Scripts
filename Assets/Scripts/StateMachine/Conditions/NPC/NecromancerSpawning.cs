
using UnityEngine;

[CreateAssetMenu(fileName = "NecromancerSpawning", menuName = "State Machine/Conditions/NPC Necromancer Spawning")]
class NecromancerSpawning : TCondition
{
    public override bool Check(Entity entity)
    {
        if (entity is not Necromancer necromancer)
        {
            return false;
        }
        return necromancer.isSpawning;
    }
}