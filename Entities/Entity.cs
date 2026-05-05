using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // stuff every entity should have
    public Vector3 velocityVector;  // for moving/applying forces to entity from scripts
    // ! if there are possible race conditions on velocityVector, consider how to implement safe locking s.t everything is applied correctly


    [SerializeField]
    protected float _health;
    public float Health { get {return _health; }}

    [SerializeField]
    private float _maxHealth = 100f;
    public float maxHealth { get {return _maxHealth; }}

    public float timeSinceLastAtk;

    // Weapon script variable
    [SerializeField]
    public Weapon weapon = default;
    
    public float knockbackForce;

    public void DealDamage(float damage)
    {
        // Debug.Log(this.name + " attacked");
        // Debug.Log("Health before: " + _health);
        // Debug.Log("Damage of attack: " + damage);
        if (damage >= _health) 
        { 
            _health = 0;
            // todo - death handling
        }
        else
        {
            _health -= damage;
        }
        // Debug.Log("Health after: " + _health);
    }
    public void ResetHP() {
        _health = maxHealth;
    }


    [SerializeField] 
    protected Animator _animator;
    public Animator animator { get {return _animator;}}

    public bool isGrounded;

    public bool attackPerformed = false;

    // managing weapon attack collisions
    public virtual void EnableWeaponCollision()
    {
        weapon.setAttackingTrue();
        // Debug.Log("Weapon collision enabled");
    }

    // argument animator param to set false - more modular
    public virtual void DisableWeaponCollision(string animatorParam)
    {
        attackPerformed = true;
        animator.SetBool(animatorParam, false);
        weapon.setAttackingFalse();
        // Debug.Log("Weapon collision disabled");
    }

    public void cancelAttack()
    {
        attackPerformed = true;
        animator.SetTrigger("CancelAttack");
        weapon.SetDamage(0f);
        weapon.SetKnockback(0f);
        weapon.setAttackingFalse();
    }
}
