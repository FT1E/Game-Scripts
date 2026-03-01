using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    [SerializeField]
    private InputReader _inputReader = default;

    [SerializeField]
    protected Transform _cameraTransform;
    public Transform cameraTransform { get { return _cameraTransform; } }


    // INPUT READ VARIABLES
    
    
    // input direction read is rotated relative to camera
    new public Vector2 moveDirection { get { return Quaternion.AngleAxis(cameraTransform.eulerAngles.y, Vector3.up) * _moveDirection; } }
    

    // END INPUT READ VARIABLES

    private void OnEnable()
    {
        _inputReader.moveEvent += OnMove;
        _inputReader.runStartEvent += RunStart;
        _inputReader.runStopEvent += RunStop;
        _inputReader.jumpStartEvent += JumpStart;
        _inputReader.jumpCancelEvent += JumpCancel;
        _inputReader.attackEvent += AttackTrigger;

        _inputReader.EnableGameplay();
    }

    private void OnDisable()
    {
        _inputReader.moveEvent -= OnMove;
        _inputReader.runStartEvent -= RunStart;
        _inputReader.runStopEvent -= RunStop;
        _inputReader.jumpStartEvent -= JumpStart;
        _inputReader.jumpCancelEvent -= JumpCancel;
        _inputReader.attackEvent -= AttackTrigger;

        _inputReader.DisableInputSystem();
    }

    private void OnMove(Vector2 input)
    {
        _moveDirection = input;
    }
    private void RunStart()
    {
        isRunning = true;
    }

    private void RunStop()
    {
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

    private void AttackTrigger() { 
        attackTrigger = true;
    }
}
