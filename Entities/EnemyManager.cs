using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour {
    
    // todo - object pooling

    [SerializeField]
    private PlayerInfo playerInfo = default;
    // above is for setting target position of NavMeshAgents

    void Awake()
    {
        
    }

    public void SpawnEnemy(int count)
    {
        throw new System.NotImplementedException();
    }

    

    void Update()
    {
        
    }
}