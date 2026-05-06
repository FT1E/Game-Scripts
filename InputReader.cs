using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
public class InputReader : ScriptableObject, PlayerInputSystem.IGameplayActions
{

    public event UnityAction attackEvent = delegate { };
    public event UnityAction lightAttackEvent = delegate { };
    public event UnityAction<Vector2> cameraMoveEvent = delegate { };
    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction runStartEvent = delegate { };
    public event UnityAction runStopEvent = delegate { };
    public event UnityAction jumpStartEvent = delegate { };
    public event UnityAction jumpCancelEvent = delegate { };
    public event UnityAction shieldEvent = delegate { };


    private PlayerInputSystem playerInput;
    

    private void OnEnable()
    {
        if (playerInput == null) { 
            playerInput = new PlayerInputSystem();
            playerInput.Gameplay.SetCallbacks(this);
        }
    }


    public void EnableGameplay() {
        playerInput.Gameplay.Enable();
    }

    public void DisableInputSystem() { 
        playerInput.Gameplay.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) {
            attackEvent.Invoke();
        }
    }

    public void OnCameraMove(InputAction.CallbackContext context)
    {
        cameraMoveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) {
            runStartEvent.Invoke();    
        }else if (context.phase == InputActionPhase.Canceled) {
            runStopEvent.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Performed)
        {
            jumpStartEvent.Invoke();
        }
        else if (context.phase == InputActionPhase.Canceled) {
            jumpCancelEvent.Invoke();
        }
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) {
            lightAttackEvent.Invoke();
        }
    }

    public void OnShield(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) {
            shieldEvent.Invoke();
        }
    }
}
