
using UnityEngine;

[CreateAssetMenu(fileName = "Grounded", menuName = "State Machine/Conditions/Character Grounded")]
class Grounded : TCondition
{
    public override bool Check(Entity entity)
    {
        return entity.isGrounded;
    }
}