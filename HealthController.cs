using UnityEngine;

public class HealthController : MonoBehaviour
{
    // will have an option about whether to show an entity's health bar
    [SerializeField] private GameObject ui_health;

    [SerializeField] private float max_health = 5.0f;
    private float current_health = 0;


    // layer checking and checking whether one entity can deal damage
    // is not done here - like mob enemy damaging another mob enemy and other stuff
    // this method is called after that checking
    public void DealDamage(float damage)
    {
        if (damage >= current_health) { 
            current_health = 0;
            TriggerDeath();
        }
        else
        {
            current_health -= damage;
        }
        UpdateHealthBar();
    }

    private void TriggerDeath()
    {
        // todo - for all the stuff which happens when a character dies
        // like set animator param
        // change the layers and stuff so the player can't interact with it - pointless to call DealDamage on a dead enemy
        // destroy game object when player can't see it - need to figure this out
        // 
    }

    private void UpdateHealthBar()
    {
        // todo - after implement the health bar gameobject
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
