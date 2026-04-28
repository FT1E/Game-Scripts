using UnityEngine;


[CreateAssetMenu(fileName = "LightAttackTrigger", menuName = "State Machine/Conditions/Light Attack Triggered")]
public class LightAttackTrigger : TCondition
{
    public override bool Check(Entity entity)
    {
        if (entity is Player p)
        {
            return p.lightAttackTrigger;
        }
        return false;
    }
}
