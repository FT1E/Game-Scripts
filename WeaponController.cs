using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] private float damage = 1f;


    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        Debug.Log(this.name + " collided with " + other.name);
        if (other.layer == 7)
        {
            // layer name == "Enemy"
            Debug.Log("Collided with enemy! Doing attack damage!");
            other.GetComponent<HealthController>().DealDamage(damage);

        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
