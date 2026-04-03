using UnityEngine;
using UnityEngine.AI;

public class NavMeshMoveState : MState
{
    private readonly PlayerInfo playerInfo;

    public NavMeshMoveState(PlayerInfo playerInfo)
    {
        this.name = "NavMesh Move State";
        this.playerInfo = playerInfo;
    }

    protected override void onEnter(Entity entity)
    {
        if (entity is not NPCEntity npc) return;
        npc.nmAgent.Warp(npc.transform.position);
        npc.nmAgent.isStopped = false;
    }

    protected override void onExit(Entity entity)
    {
        if (entity is not NPCEntity npc) return;
        npc.nmAgent.isStopped = true;
    }

    protected override void onUpdate(Entity entity)
    {
        if (entity is not NPCEntity npc) return;
        NavMeshAgent agent = npc.nmAgent;

        agent.destination = playerInfo.position;
        // agent.remainingDistance;     // todo - for distance checking might be better to use this rather than Vector3.distance, since this uses the distance on the path to walk to player
        // agent.SetAreaCost            // todo - interesting for modifying the movement behaviour
        // agent.Warp                   // todo

    }
}