using UnityEngine;

[CreateAssetMenu(fileName = "TimeSinceLastAttack", menuName = "State Machine/Conditions/Time in seconds since last attack")]
class TimeSinceLastAttack : TCondition
{
    [Tooltip("Number of seconds since last attack must pass for true")]
    [SerializeField]
    private float seconds = 1f;

    public override bool Check(Entity entity)
    {
        return entity.timeSinceLastAtk >= seconds;
    }
}