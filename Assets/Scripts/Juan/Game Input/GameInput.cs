using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    InputSystem_Actions inputActions;

    public Vector2 MovementInput { get; private set; }

    public event Action OnInteractPerformed;


    void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;

        inputActions.Player.Interact.performed += OnInteractPerformedCallback;
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

        inputActions.Player.Interact.performed -= OnInteractPerformedCallback;
    }

    void OnMovePerformed(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MovementInput = Vector2.zero;
    }

    void OnInteractPerformedCallback(InputAction.CallbackContext context)
    {
        OnInteractPerformed?.Invoke();
    }
}
