using UnityEngine;

public class Spell : Weapon
{
    [SerializeField]
    private PlayerInfo playerInfo;

    [SerializeField]
    private float jitter=0f;


    [SerializeField]
    private bool moveSpell = false;
    
    [SerializeField]
    private SpellCollider spellCollider;

    private ParticleSystem particles;
    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        if (moveSpell)
        {
            transform.position = playerInfo.position + new Vector3(Random.Range(-jitter, jitter), 0, Random.Range(-jitter, jitter));
        }
        particles.Play();
    }

    public override void SetDamage(float damage)
    {
        spellCollider.SetDamage(damage);
    }

    public override void SetKnockback(float knockbackForce)
    {
        spellCollider.SetKnockback(knockbackForce);
    }

    public override void clearHits()
    {
        spellCollider.clearHits();
    }

    public override void SetHitLayer(int layer)
    {
        spellCollider.SetHitLayer(layer);
    }

    public override void setAttackingFalse()
    {
        spellCollider.setAttackingFalse();
    }

    public override void setAttackingTrue()
    {
        spellCollider.setAttackingTrue();
    }

}