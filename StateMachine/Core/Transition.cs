using Unity.VisualScripting;
using UnityEngine;
using System;


[Serializable]
public class Transition
{
    [SerializeField]
    public StateSO toState;
    [SerializeField]
    public TCondition condition;
}
