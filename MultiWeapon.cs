using UnityEngine;

public class MultiWeapon : Weapon
{
    [SerializeField]
    private Weapon[] weapons;

    public override void SetDamage(float damage)
    {
        foreach(Weapon weapon in weapons)
        {
            weapon.SetDamage(damage);
        }
    }

    public override void SetKnockback(float knockbackForce)
    {
        foreach(Weapon weapon in weapons)
        {
            weapon.SetKnockback(knockbackForce);
        }
    }

    public override void clearHits()
    {
        foreach(Weapon weapon in weapons)
        {
            weapon.clearHits();
        }
    }
    public override void SetHitLayer(int layer)
    {
        foreach(Weapon weapon in weapons)
        {
            weapon.SetHitLayer(layer);
        }
    }

    public override void setAttackingFalse()
    {
        foreach(Weapon weapon in weapons)
        {
            weapon.setAttackingFalse();
        }
    }

    public override void setAttackingTrue()
    {
        foreach(Weapon weapon in weapons)
        {
            weapon.setAttackingTrue();
        }
    }
}