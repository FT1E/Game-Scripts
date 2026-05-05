using UnityEngine;

public class SpellCollider : Weapon
{
    void OnParticleCollision(GameObject other)
    {
        onHit(other);
    }
}