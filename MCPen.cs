using UnityEngine;

public class MCPen : Weapon
{

    private void Awake()
    {
        if (attackDamage == null)
        {
            attackDamage = new float[4];
            attackAnimationLength = new float[4];

            for (int i = 0; i < 4; i++)
            {
                attackDamage[i] = (i + 1) * 10f;
            }

            // todo
            // will figure out a less tedious way to do this
            attackAnimationLength[0] = 1.267f;
            attackAnimationLength[1] = 1.833f;
            attackAnimationLength[2] = 1.8f;
            attackAnimationLength[3] = 2.133f;
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
