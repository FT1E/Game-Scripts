using UnityEngine;

public class Entity : MonoBehaviour
{
    // stuff every entity should have
    public Vector3 velocityVector;  // for moving/applying forces to entity from scripts
    // ! if there are possible race conditions on velocityVector, consider how to implement safe locking s.t everything is applied correctly


    [SerializeField]
    protected float _health;
    public float Health { get {return _health; }}

    
    // Weapon script variable
    [SerializeField]
    public Weapon weapon = default;

    public void DealDamage(float damage)
    {
        Debug.Log(this.name + " attacked");
        Debug.Log("Health before: " + _health);
        Debug.Log("Damage of attack: " + damage);
        if (damage >= _health) 
        { 
            _health = 0;
            // todo - death handling
        }
        else
        {
            _health -= damage;
        }
        Debug.Log("Health after: " + _health);
    }

    [SerializeField] 
    protected Animator _animator;
    public Animator animator { get {return _animator;}}

    protected bool _isGrounded = true;
    public bool isGrounded { get {return _isGrounded; }}
    

    public bool attackPerformed = false;

    // managing weapon attack collisions
    public virtual void EnableWeaponCollision()
    {
        weapon.setAttackingTrue();
        Debug.Log("Weapon collision enabled");
    }

    // todo - argument animator param to set false - more modular
    public virtual void DisableWeaponCollision(string animatorParam)
    {
        attackPerformed = true;
        animator.SetBool(animatorParam, false);
        weapon.setAttackingFalse();
        Debug.Log("Weapon collision disabled");
    }
}
