using UnityEngine;

[CreateAssetMenu(fileName = "InAir", menuName = "State Machine/Conditions/Character In Air (Not grounded)")]
class InAir : TCondition
{
    public override bool Check(Entity entity)
    {
        return !entity.Grounded();
    }
}