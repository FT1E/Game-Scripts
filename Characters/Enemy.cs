using UnityEngine;

class Enemy : Character
{

    [SerializeField]
    private PlayerInfo playerInfo = default;

    // temp
    private Vector3 temp;

    void Start()
    {
        
    }

    void Update()
    {
        temp = (playerInfo.position - transform.position).normalized;
        _moveDirection = new Vector2(temp.x, temp.z);
        // Debug.Log($"{name}: {playerInfo.position.ToString()}");
        // Debug.Log($"{name}: {transform.position.ToString()}");
        // Debug.Log($"{name}: {_moveDirection.ToString()}");
        Debug.Log($"{name} from Enemy: {velocityVector.ToString()}");
    }
}