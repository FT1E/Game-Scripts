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


    [SerializeField] private float cam_rotation_speed = 1.0f;
    private Vector3 cam_rotation_delta = Vector3.zero;
    [SerializeField] private GameObject CameraRotator;    
    private bool LeftClickDown = false;

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

        var MouseDelta = playerInputSystem.BaseMap.MouseDelta;
        MouseDelta.performed += RotateCam;
        MouseDelta.canceled += context => cam_rotation_delta = Vector3.zero;

        var RightClick = playerInputSystem.BaseMap.RightClick;
        RightClick.started += context => LeftClickDown = true;
        RightClick.canceled += context => LeftClickDown = false;
    }


    private void MoveAction(InputAction.CallbackContext context) { 
        Vector2 keys_vector2 = context.ReadValue<Vector2>();
        move_direction.x = keys_vector2.x;
        move_direction.z = keys_vector2.y;

        animator.SetFloat("MBF", move_direction.z);
        animator.SetFloat("MLR", move_direction.x);
    }

    private void Attack1Action(InputAction.CallbackContext context)
    {
        animator.SetTrigger("Attack1");

    }

    private void RotateCam(InputAction.CallbackContext context){
        if (!LeftClickDown){
            Vector2 mouse_delta = context.ReadValue<Vector2>();

            // DEBUG
            // Debug.Log("Inside RotateCam, vector2 values (x,y):");
            // Debug.Log(mouse_delta.x);
            // Debug.Log(mouse_delta.y);


            // cam_rotation_delta.x = mouse_delta.y;
            cam_rotation_delta.y = mouse_delta.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdatePos();
        UpdateCam();
    }

    private void UpdatePos() {
        if (move_direction != Vector3.zero)
        {
            if(CameraRotator.transform.rotation.eulerAngles != Vector3.zero){
                transform.Rotate(CameraRotator.transform.localRotation.eulerAngles); 
                CameraRotator.transform.localRotation = Quaternion.Euler(Vector3.zero);
            }
            characterController.Move(transform.rotation * move_direction * speed * Time.deltaTime);
        }
    }
    private void UpdateCam(){
        if(cam_rotation_delta != Vector3.zero){
            CameraRotator.transform.Rotate(cam_rotation_delta * cam_rotation_speed * Time.deltaTime);
        }
    }

}
