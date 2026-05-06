using UnityEngine;

public class Projectile : Weapon
{
    // todo - think about if you want some kind of physics applied on this

    // one of the main differences:
    //  *- after 1 hit it is disabled
    //  *- it has timeout - if it doesn't hit anything for 5s (or other time) it dissapears
    //  *- 

    public ProjectileManager poolManager;
    private Vector3 direction;

    [SerializeField]
    float speed = 2f;

    [SerializeField]
    private float timeout = 5f;     // in seconds
    // todo - experiment with above
    private float timeAlive = 0f;

    void OnEnable()
    {
        timeAlive = 0f;
        attacking = true;
    }

    void OnDisable()
    {
        attacking = false;
        SetDamage(0f);
        SetKnockback(0f);
    }
    public void SetDirection(Vector3 direction){
        this.direction = direction;
    }

    protected override void onHit(GameObject hit){
        Debug.Log("Projectile hit: " + hit.name);
        base.onHit(hit);
        poolManager.DisableProjectile(this); // * disables this and adds it to disabled objects
    }

    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= timeout)
        {
            poolManager.DisableProjectile(this); // * disables this and adds it to disabled objects
        }
        transform.position += speed * direction * Time.deltaTime;
    }
}