using UnityEngine;


[CreateAssetMenu(fileName = "JumpTrigger", menuName = "State Machine/Conditions/Jump Triggered")]
public class JumpTrigger : TCondition
{
    public override bool Check(Entity entity)
    {
        if (entity.GetPlayer() is Player p)
        {
            return p.jumpTrigger;
        }
        return false;
    }
}
