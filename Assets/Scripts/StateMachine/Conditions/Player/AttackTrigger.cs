using UnityEngine;


[CreateAssetMenu(fileName = "AttackTrigger", menuName = "State Machine/Conditions/Attack Triggered")]
public class AttackTrigger : TCondition
{
    public override bool Check(Entity entity)
    {
        if (entity is Player p)
        {
            return p.attackTrigger;
        }
        return false;
    }
}
