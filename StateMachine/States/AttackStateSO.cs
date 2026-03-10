using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackStateSO", menuName = "Scriptable Objects/State Machine/States/Attack State")]
public class AttackStateSO : StateSO
{
    [SerializeField]
    private float damage;

    [SerializeField]
    private float clipLength=0f;
    
    [Tooltip("Pass the attack animation clip here if you don't know the exact lenght of the animation")]
    [SerializeField]
    private AnimationClip animationClip;

    [Tooltip("Name of the animator parameter used to start the attack animation")]
    [SerializeField]
    private String animatorParameter;


    private AttackState attackState {get { return (AttackState) _state;}}

    public void OnEnable()
    {
        
        if (_state == null)
        {
            if(clipLength == 0f && animationClip != null)
            {
                clipLength = animationClip.length;
            }
            _state = new AttackState(damage, clipLength, animatorParameter);
        }
    }

    public override StateSO checkTransitions(StateMachine stateMachine)
    {
        if(attackState.performed){
            return base.checkTransitions(stateMachine);
        }
        return null;
    }

}
