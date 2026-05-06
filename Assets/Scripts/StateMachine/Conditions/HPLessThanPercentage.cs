
using UnityEngine;

[CreateAssetMenu(fileName = "HPLessThanPercentage", menuName = "State Machine/Conditions/HP Less Than Percent")]
class HPLessThanPercentage : TCondition
{
    [Tooltip("Range from 0-100")]
    [SerializeField]
    private float maxTruePercent = 0f;

    public override bool Check(Entity entity)
    {
        return entity.Health / entity.maxHealth * 100 <= maxTruePercent;
    }
}