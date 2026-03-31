using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    
    // key is GameObject hashcode from GetHashCode
    private Dictionary<int, Enemy> activeEnemies;
    private Dictionary<int, Enemy> disabledEnemies;
    // when needing to spawn new enemies, grab some from disabled if available


    void Awake()
    {
        foreach(Transform child in transform)
        {
            disabledEnemies.Add(child.gameObject.GetHashCode(), new Enemy());
        }
    }

    public void SpawnEnemy(int count)
    {
        // argument - how many enemies to spawn
        Enemy enemy;
        foreach(KeyValuePair<int, Enemy> pair in disabledEnemies)
        {
            if (count <=0) break;
            count--;
            enemy = pair.Value;
            activeEnemies.Add(pair.Key, pair.Value);
            disabledEnemies.Remove(pair.Key);


            /* 
            TODO
                set active and some initial values
                health - 100%
                transform 
                    - position within some radius to player, not too close though
                    - rotation - 0,0,0 
                    - scale - I don't think I'll do anything with it anytime, maybe with bosses, but this is for mob enemies
                also velocity vector - initially 0,0,0
            */
        }
    }

    public void dealDamage(int enemyHash, float damage)
    {
        Enemy enemy;
        if(activeEnemies.TryGetValue(enemyHash, out enemy))
        {
            enemy.dealDamage(damage);
            if(enemy.Health <= 0)
            {
                // todo - kill/death stuff
                // todo - remove it from active, add it to deactive (object pooling), maybe after a delay (maybe have in-between state for active dead enemies, which are eventually going to deactive)
            }
        }
    }

    void Update()
    {
        Enemy enemy;
        foreach(KeyValuePair<int, Enemy> pair in activeEnemies){
            enemy = pair.Value;

            // todo 
            //  *- state machine 
            //      - transition check & update state if needed
            //      - state action

            

        }
    }
}