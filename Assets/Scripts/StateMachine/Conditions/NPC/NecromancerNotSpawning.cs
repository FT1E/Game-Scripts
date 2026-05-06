
using UnityEngine;

[CreateAssetMenu(fileName = "NecromancerNotSpawning", menuName = "State Machine/Conditions/NPC Necromancer Not Spawning")]
class NecromancerNotSpawning : TCondition
{
    public override bool Check(Entity entity)
    {
        if (entity is not Necromancer necromancer)
        {
            return false;
        }
        return !necromancer.isSpawning;
    }
}