using UnityEngine;
using UnityEngine.Events;

public class CombatAnimationEvents : MonoBehaviour
{
    public event UnityAction WindUpEnd;
    public event UnityAction<string> WindDownStart;
    public event UnityAction End;
    

    public void WindUp()
    {
        WindUpEnd?.Invoke();
    }

    public void WindDown(string animatorParam)
    {
        WindDownStart?.Invoke(animatorParam);
    }
    public void AnimationEnd()
    {
        End?.Invoke();
    }
}
