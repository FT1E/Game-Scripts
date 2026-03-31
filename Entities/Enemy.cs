using UnityEngine;

public class Enemy : Entity
{
    
    public void dealDamage(float damage)
    {
        if(damage >= _health) {
            _health = 0;
            return;
        }
        _health -= damage;
    }
}