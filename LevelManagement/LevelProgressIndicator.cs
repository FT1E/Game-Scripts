using UnityEngine;

public class LevelProgressIndicator : MonoBehaviour
{
    [SerializeField]
    private LevelManagerSO levelManagerSO;

    private ParticleSystem particles;
    private float last_progress = -1f;
    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(last_progress != levelManagerSO.progress)
        {
            last_progress = levelManagerSO.progress;    
            // don't know why, but it gives error when trying below in 1 line 
            var main = particles.main;
            main.startColor = new Color(1-last_progress, last_progress, 1 - last_progress);
        }
    }
}