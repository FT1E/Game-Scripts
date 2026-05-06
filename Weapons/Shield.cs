using UnityEngine;


public class Shield : MonoBehaviour
{
    [SerializeField]
    private float timeout = 5f; // how many seconds until shield can be used again
    private float timeSinceLastCast = 5f;   // default value so player can cast right from the start
    public float cooldown { 
        get
        {
            if(timeSinceLastCast >= timeout) return 0f;
            return timeout - timeSinceLastCast;
        }
        }

    [SerializeField]
    private Collider sphereCollider;
    [SerializeField]
    private ParticleSystem particles;

    private void Awake() {
        sphereCollider = GetComponent<Collider>();
        sphereCollider.enabled = false;
        particles = GetComponent<ParticleSystem>();
    }

    public bool Activate()
    {
        if(timeSinceLastCast < timeout) return false;
        if(particles.isPlaying) return false;
        particles.Play();
        sphereCollider.enabled = true;
        return true;
    }

    private void OnParticleSystemStopped()
    {
        sphereCollider.enabled = false;
        timeSinceLastCast = 0f;
    }

    void Update()
    {
        timeSinceLastCast += Time.deltaTime;
    }
}