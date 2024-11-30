using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    InputSystem_Actions inputActions;

    public Vector2 MovementInput { get; private set; }

    void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void OnDestroy()
    {
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
    }

    void OnMovePerformed(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MovementInput = Vector2.zero;
    }
}
