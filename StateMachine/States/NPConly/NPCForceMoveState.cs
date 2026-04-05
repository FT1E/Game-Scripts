using UnityEngine;
using UnityEngine.AI;

public class NPCForceMoveState : MState
{
    private readonly PlayerInfo playerInfo;
    

    public NPCForceMoveState(PlayerInfo playerInfo)
    {
        this.name = "NPC Force Move State";
        this.playerInfo = playerInfo;
    }

    protected override void onEnter(Entity entity)
    {
        if (entity is not NPCEntity npc) return;
        // set entity velocityVector to knockback force applied
        Vector3 forceVector = (npc.transform.position - playerInfo.position).normalized;
        forceVector.y = 0f;
        forceVector = forceVector.normalized;
        npc.velocityVector = (forceVector + Vector3.up).normalized * npc.knockbackForce;
        npc.knockbackForce = 0f;
        npc.nmAgent.enabled = false;
        npc.manualMove = true;
    }

    protected override void onExit(Entity entity)
    {
        if (entity is not NPCEntity npc) return;
        npc.nmAgent.enabled = true;
    }

    protected override void onUpdate(Entity entity)
    {
        Debug.Log("Inside update npc force move state");
        Debug.Log(entity.velocityVector);

        if (entity is not NPCEntity npc) return;
        NavMeshAgent agent = npc.nmAgent;

        entity.transform.position += npc.velocityVector * Time.deltaTime;

        MyPhysics.ApplyDragOnVelocityVector(entity);
        MyPhysics.ApplyGravity(entity);
        // Vector3.Lerp(Vector3.zero, npc.velocityVector, 0.5f);

        npc.manualMove = npc.velocityVector != Vector3.down;
    }
}