using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackStateSO", menuName = "State Machine/States/Attack State")]
public class AttackStateSO : StateSO
{
    [SerializeField]
    private float damage;

    [Tooltip("Name of the animator parameter used to start the attack animation")]
    [SerializeField]
    private String animatorParameter;

    [SerializeField]
    private float knockbackForce = 0f;

    [SerializeField]
    private float rotationSpeed=540f;

    [SerializeField]
    private bool moveAllowed = false;

    public void OnEnable()
    {
        
        if (_state == null)
        {
            _state = new AttackState(damage, animatorParameter, knockbackForce, rotationSpeed, moveAllowed);
        }
    }

    public override StateSO checkTransitions(Entity entity)
    {
        if(entity.attackPerformed){
            return base.checkTransitions(entity);
        }
        return null;
    }

}
