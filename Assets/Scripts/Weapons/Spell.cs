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

    public void Play(Transform caster)
    {
        if (moveSpell)
        {
            transform.position = playerInfo.position + new Vector3(Random.Range(-jitter, jitter), 0, Random.Range(-jitter, jitter));
        }else
        {
            transform.position = caster.position;
            transform.rotation = caster.rotation;
            transform.Rotate(0, -90, 0); // so that the spell faces forward instead of to the right
            // only 1 spell goes here, and it needs to be rotated -90 on y
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