using System;
using UnityEngine;

// for now do nothing
// may be used for initial state, and as a bridge connecting 1-action states
// like for example JumpState, just set jump -> idle -> in-airMove (or just stay in idle if grounded)
// basically like a central state connected to (almost) every other state

[CreateAssetMenu(fileName = "RandomStateSO", menuName = "State Machine/States/Random State Container")]
public class RandomStateSO : StateSO
{
    // * Note: transitions is kinda useless here, so don't add anything to it

    // kind of like a place holder
    // get in there for 1 frame
    // next transition to 1 random state of those in the array

    [SerializeField]
    private StateSO[] states;

    // temp
    private System.Random random;
    
    
    
    void OnEnable()
    {
        if(random == null) random = new System.Random();
        if(_state == null) _state = new IdleState();    // so it's not null
    }

    // empty conditions
    public override StateSO checkTransitions(Entity entity)
    {
        return states[random.Next(states.Length)];
    }

}
