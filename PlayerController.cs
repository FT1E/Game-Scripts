using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    PlayerInputSystem playerInputSystem;
    [SerializeField] private float speed = 5.0f;
    Vector3 move_direction = Vector3.zero;
    Animator animator;

    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();
    }

    private void OnEnable()
    {
        playerInputSystem.BaseMap.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void MoveAction(InputAction.CallbackContext context) { 
        Vector2 keys_vector2 = context.ReadValue<Vector2>();
        move_direction.x = keys_vector2.x;
        move_direction.z = keys_vector2.y;
        animator.SetBool("IsMoving", move_direction != Vector3.zero);
        
    }
    // Update is called once per frame
    void Update()
    {
        var MoveMap = playerInputSystem.BaseMap.Move;
        //MoveMap.started += MoveAction;
        MoveMap.performed += MoveAction;
        MoveMap.canceled += MoveAction;
        
        UpdatePos();
    }

    private void UpdatePos() {
        transform.position += speed * move_direction * Time.deltaTime;
    }
}
