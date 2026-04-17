
using UnityEngine;

[CreateAssetMenu(fileName = "HPLessThanValue", menuName = "State Machine/Conditions/HP Less Than Value")]
class HPLessThanValue : TCondition
{
    [SerializeField]
    private float maxTrueValue = 0f;

    public override bool Check(Entity entity)
    {
        return entity.Health <= maxTrueValue;
    }
}