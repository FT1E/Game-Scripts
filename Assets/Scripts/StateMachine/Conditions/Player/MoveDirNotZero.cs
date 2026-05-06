using UnityEngine;

[CreateAssetMenu(fileName = "MoveDirNotZero", menuName = "State Machine/Conditions/Move Direction Not Zero")]
class MoveDirNotZero : TCondition
{
    public override bool Check(Entity entity)
    {
        if (entity is Player p)
        {
            return p.MoveDirection != Vector2.zero;
        }
        return false;
    }
}