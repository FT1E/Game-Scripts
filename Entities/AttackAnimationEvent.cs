using UnityEngine;
using UnityEngine.Events;

public class AttackAnimationEvent : MonoBehaviour
{
    public event UnityAction WindUpEnd;
    public event UnityAction WindDownStart;
    public event UnityAction End;
    

    public void WindUp()
    {
        WindUpEnd?.Invoke();
    }

    public void WindDown()
    {
        WindDownStart?.Invoke();
    }
    public void AnimationEnd()
    {
        End?.Invoke();
    }
}
