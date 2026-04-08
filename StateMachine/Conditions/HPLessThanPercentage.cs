
using UnityEngine;

[CreateAssetMenu(fileName = "HPLessThanPercentage", menuName = "State Machine/Conditions/HP Less Than Percent")]
class HPLessThanPercentage : TCondition
{
    [SerializeField]
    private readonly float maxTruePercent = 0f;

    public override bool Check(Entity entity)
    {
        return entity.Health / entity.maxHealth <= maxTruePercent;
    }
}