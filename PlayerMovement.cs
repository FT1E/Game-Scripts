using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // other components
    private CharacterController characterController;
    [SerializeField] private Animator animator = default;
    [SerializeField] private float animatorMaxSpeed = 2f;

    // input data
    [SerializeField] private InputReader _inputReader = default;
    private Vector3 moveDirection = Vector2.zero;
    private Boolean isRunning = false;

    // non-y-vertical position data
    [SerializeField] private float currentSpeed = 0f;
    [SerializeField] private float maxWalkingSpeed = 2f;
    [SerializeField] private float maxRunningSpeed = 5.0f;
    [SerializeField] private float walkAcceleration = 0.5f;
    [SerializeField] private float runAcceleration = 1f;

    private Vector3 moveVector = Vector3.zero;

    // vertical/y positioning
    private readonly float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3f;
    private bool jumpTrigger = false;
    [SerializeField] private float jumpPushPower = 20f;
    private float verticalSpeed = 0f;

    // rotation data
    private float currentAspect = 0f;   // rotation.y value - i.e. where the player is currently facing
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private Transform cameraTransform = default;

    private void OnEnable()
    {
        _inputReader.moveEvent += OnMove;
        _inputReader.runStartEvent += RunStart;
        _inputReader.runStopEvent += RunStop;
        _inputReader.jumpStartEvent += JumpStart;
        _inputReader.jumpCancelEvent += JumpCancel;

        _inputReader.EnableGameplay();
    }

    private void OnDisable()
    {
        _inputReader.moveEvent -= OnMove;
        _inputReader.runStartEvent -= RunStart;
        _inputReader.runStopEvent -= RunStop;
        _inputReader.jumpStartEvent -= JumpStart;
        _inputReader.jumpCancelEvent -= JumpCancel;

        _inputReader.DisableInputSystem();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        ApplyRotation();
        CalculateVerticalChange();
        setAnimatorSpeed();
        UpdatePos();
    }


    private void CalculateMovement() {
        if (moveDirection != Vector3.zero)
        {
            if (isRunning)
            {
                if (currentSpeed < maxRunningSpeed)
                {
                    currentSpeed += runAcceleration * Time.deltaTime;
                }
                else
                {
                    currentSpeed = maxRunningSpeed;
                }
            }
            else
            {
                currentSpeed = maxWalkingSpeed;
            }

            moveVector = moveDirection * currentSpeed;
        }
        else {
            moveVector = Vector3.zero;
        }
    }

    private void CalculateVerticalChange()
    {
        if (jumpTrigger)
        {
            jumpTrigger = false;
            verticalSpeed = jumpPushPower;
        }

        if (characterController.isGrounded && verticalSpeed <= 0.1f)
        {
            verticalSpeed = -1f;
        }
        else
        {
            verticalSpeed += gravity * gravityMultiplier * Time.deltaTime;
        }

        moveVector.y += verticalSpeed;
    }


    private void ApplyRotation() {
        if (moveVector != Vector3.zero) {
            // apply camera rotation on the movement vector
            moveVector = Quaternion.AngleAxis(cameraTransform.eulerAngles.y, Vector3.up) * moveVector;

            // rotate the character toward the movement

            //float angle = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg;

            //transform.forward = moveVector;

            Quaternion lookRotation = Quaternion.LookRotation(moveVector);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void setAnimatorSpeed() {
        if (moveDirection == Vector3.zero) {
            animator.SetFloat("speed", 0f);
        }else if (!isRunning)
        {
            animator.SetFloat("speed", 0.1f);
        }
        else {
            animator.SetFloat("speed", (currentSpeed / maxRunningSpeed) * animatorMaxSpeed);
        }

    }
    private void UpdatePos() {

        characterController.Move(moveVector * Time.deltaTime);
    }


    private void OnMove(Vector2 input) {
        moveDirection.x = input.x;
        moveDirection.z = input.y;
    }
    private void RunStart() {
        isRunning = true;
    }

    private void RunStop() { 
        isRunning = false;
    }

    private void JumpStart()
    {
        jumpTrigger = true;
    }

    private void JumpCancel()
    {
        jumpTrigger = false;
    }
}
