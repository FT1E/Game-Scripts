using UnityEngine;

public class SpellCaster : Weapon
{

    [SerializeField]
    private Spell[] spells;


    private void Awake()
    {
        
    }    

    public void CastSpell(int i)
    {
        spells[i].setAttackingTrue();
        spells[i].Play();
    }
    
    public override void SetDamage(float damage)
    {
        foreach(Spell spell in spells)
        {
            spell.SetDamage(damage);
        }
    }

    public override void SetKnockback(float knockbackForce)
    {
        foreach(Spell spell in spells)
        {
            spell.SetKnockback(knockbackForce);
        }
    }

    public override void clearHits()
    {
        foreach(Spell spell in spells)
        {
            spell.clearHits();
        }
    }
    public override void SetHitLayer(int layer)
    {
        foreach(Spell spell in spells)
        {
            spell.SetHitLayer(layer);
        }
    }

    public override void setAttackingFalse()
    {
        foreach(Spell spell in spells)
        {
            spell.setAttackingFalse();
        }
    }

    public override void setAttackingTrue()
    {
        foreach(Spell spell in spells)
        {
            spell.setAttackingTrue();
        }
    }
}