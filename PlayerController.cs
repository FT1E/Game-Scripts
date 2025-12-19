using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    private Animator weaponAnimator;
    
    
    
    private PlayerInputSystem playerInputSystem;
    private Animator animator;
    private CharacterController characterController;

    [SerializeField] private float speed = 5.0f;
    Vector3 move_direction = Vector3.zero;

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
        characterController = GetComponent<CharacterController>();
        
        var MoveMap = playerInputSystem.BaseMap.Move;
        //MoveMap.started += MoveAction;
        MoveMap.performed += MoveAction;
        MoveMap.canceled += MoveAction;

        var Attack1 = playerInputSystem.BaseMap.Attack1;
        Attack1.performed += Attack1Action;
    }


    private void MoveAction(InputAction.CallbackContext context) { 
        Vector2 keys_vector2 = context.ReadValue<Vector2>();
        move_direction.x = keys_vector2.x;
        move_direction.z = keys_vector2.y;
        animator.SetBool("IsMoving", move_direction != Vector3.zero);
    }

    private void Attack1Action(InputAction.CallbackContext context)
    {
        animator.SetTrigger("Attack1");

    }

    // Update is called once per frame
    void Update()
    {
        
        UpdatePos();
    }

    private void UpdatePos() {
        if (move_direction != Vector3.zero)
        {
            characterController.Move(speed * move_direction * Time.deltaTime);
        }
    }

}
