using UnityEngine;
using UnityEngine.AI;

public class NPCEntity : Entity
{
    protected NavMeshAgent _nmAgent;
    public NavMeshAgent nmAgent { get{return _nmAgent;} }

    public bool manualMove;

    void Awake()
    {
        _nmAgent = GetComponent<NavMeshAgent>();
    }


}