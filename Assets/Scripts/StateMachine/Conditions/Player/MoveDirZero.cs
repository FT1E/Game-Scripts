using UnityEngine;

[CreateAssetMenu(fileName = "MoveDirZero", menuName = "State Machine/Conditions/Move Direction == Zero")]
class MoveDirZero : TCondition
{
    public override bool Check(Entity entity)
    {
        if (entity is Player p)
        {
            return p.MoveDirection == Vector2.zero;
        }
        return false;
    }
}