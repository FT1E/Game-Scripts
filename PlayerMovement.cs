using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // other components
    private CharacterController characterController;
    private Animator animator;
    [SerializeField] private float animatorMaxSpeed = 2f;

    // input data
    [SerializeField] private InputReader _inputReader = default;
    private Vector3 moveDirection = Vector2.zero;
    private Boolean isRunning = false;

    // position data
    [SerializeField] private float currentSpeed = 0f;
    [SerializeField] private float maxWalkingSpeed = 2f;
    [SerializeField] private float maxRunningSpeed = 5.0f;
    [SerializeField] private float walkAcceleration = 0.5f;
    [SerializeField] private float runAcceleration = 1f;

    private Vector3 moveVector = Vector3.zero;


    // rotation data
    private Vector3 currentDirection = new Vector3(0f, 0f, 1f);
    private Vector3 newRotation = new Vector3(0f, 0f, 1f);
    [SerializeField] private float rotationSpeed = 0.5f;

    private void OnEnable()
    {
        _inputReader.moveEvent += OnMove;
        _inputReader.runStartEvent += RunStart;
        _inputReader.runStopEvent += RunStop;

        _inputReader.EnableGameplay();
    }

    private void OnDisable()
    {
        _inputReader.moveEvent -= OnMove;
        _inputReader.runStartEvent -= RunStart;
        _inputReader.runStopEvent -= RunStop;
     
        _inputReader.DisableInputSystem();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        UpdateRotation();
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
                    currentSpeed += runAcceleration;
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


    private void UpdateRotation() {
        if (moveDirection != Vector3.zero) {
            float angle = Vector3.SignedAngle(currentDirection, moveVector, Vector3.up);

            this.transform.Rotate(Vector3.up, angle * Time.deltaTime);
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
        isRunning &= false;
    }
}
