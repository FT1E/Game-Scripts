using UnityEngine;
using UnityEngine.AI;

public class NavMeshMoveState : MState
{
    private readonly PlayerInfo playerInfo;
    private float speed;
    private string animParam;

    public NavMeshMoveState(PlayerInfo playerInfo, float speed = 3f, string animParam="")
    {
        this.name = "NavMesh Move State";
        this.playerInfo = playerInfo;
        this.speed = speed;
        this.animParam = (animParam == "") ? null : animParam;
    }

    protected override void onEnter(Entity entity)
    {
        if (entity is not NPCEntity npc) return;
        // Debug.Log("Npc warp position");
        // Debug.Log(npc.transform.position + Vector3.up * npc.nmAgent.baseOffset);
        npc.nmAgent.Warp(npc.transform.position + Vector3.up * npc.nmAgent.baseOffset);
        npc.nmAgent.speed = speed;
        npc.nmAgent.isStopped = false;
        if (animParam != null)
        {
            entity.animator.SetBool(animParam, true);
        }
    }

    protected override void onExit(Entity entity)
    {
        if (entity is not NPCEntity npc) return;
        npc.nmAgent.isStopped = true;
        npc.nmAgent.speed = 0f;
        if (animParam != null)
        {
            entity.animator.SetBool(animParam, false);
        }
        // rotate - toward player in case it tries to attack again but it hasn't rotated yet
        npc.transform.forward = playerInfo.position - npc.transform.position;
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