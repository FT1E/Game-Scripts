using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    [SerializeField]
    private InputReader _inputReader = default;

    [SerializeField]
    protected Transform _cameraTransform;
    public Transform cameraTransform { get { return _cameraTransform; } }

    [SerializeField]
    private PlayerInfo playerInfo = default;   // for setting the values

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

        // apply rotation - so that movement is relative to camera
        float angle = - Mathf.Deg2Rad * cameraTransform.rotation.eulerAngles.y; 
        _moveDirection =  new Vector2(
            _moveDirection.x * Mathf.Cos(angle) - _moveDirection.y * Mathf.Sin(angle),
            _moveDirection.x * Mathf.Sin(angle) + _moveDirection.y * Mathf.Cos(angle)
         );
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

    void Update()
    {
        playerInfo.SetPosition(transform.position);
    }
}
