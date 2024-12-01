using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    InputSystem_Actions inputActions;

    public Vector2 MovementInput { get; private set; }

    public event Action OnInteractPerformed;
    public event Action OnInventoryPerformed;


    void Awake()
    {
        inputActions = new InputSystem_Actions();

        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;

        inputActions.Player.Interact.performed += OnInteractPerformedCallback;

        inputActions.Player.Inventory.performed += OnInventoryPerformedCallback;
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
        inputActions.Player.Inventory.performed -= OnInventoryPerformedCallback;
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

    private void OnInventoryPerformedCallback(InputAction.CallbackContext context)
    {
        OnInventoryPerformed?.Invoke();
    }
}
