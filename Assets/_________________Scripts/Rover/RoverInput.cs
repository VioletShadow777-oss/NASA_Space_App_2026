
using UnityEngine;
using UnityEngine.InputSystem;

public class RoverInput : MonoBehaviour
{
    // Input values read by RoverBrain.
    public float movementInput;
    public float turnInput;
    public bool brakeInput;

    public PlayerInput roverInputAction;

    private InputActionMap roverActionMap;

    private InputAction movementAction;
    private InputAction turnAction;
    private InputAction brakeAction;
    private InputAction escapeAction;

    public RoverInteraction roverInteraction;

    private void Awake()
    {
        // Get the Rover action map before OnEnable subscribes.
        if (roverInputAction == null)
        {
            Debug.LogError("RoverInput: PlayerInput reference is missing.", this);
            return;
        }

        roverActionMap =
            roverInputAction.actions.FindActionMap("Rover", true);

        // Cache the actions.
        movementAction =
            roverActionMap.FindAction("Forward and Backward", true);

        turnAction =
            roverActionMap.FindAction("Turn", true);

        brakeAction =
            roverActionMap.FindAction("Brake", true);

        escapeAction =
            roverActionMap.FindAction("Escape", true);
    }

    private void OnEnable()
    {
        //SubscribeRoverActions();
    }

    private void OnDisable()
    {
        //UnsubscribeRoveActions();
    }

    public void ForwardAndBackward(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<float>();
    }

    public void Turn(InputAction.CallbackContext context)
    {
        turnInput = context.ReadValue<float>();
    }

    public void Brake(InputAction.CallbackContext context)
    {
        brakeInput = context.ReadValueAsButton();
    }

    public void EscapeAcion(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            roverInteraction.ExitRover();

        }
    }

    public void SubscribeRoverActions()
    {
        // Do not subscribe if the actions were not initialized.
        if (movementAction == null || turnAction == null || brakeAction == null)
        {
            return;
        }

        movementAction.performed += ForwardAndBackward;
        movementAction.canceled += ForwardAndBackward;

        turnAction.performed += Turn;
        turnAction.canceled += Turn;

        brakeAction.performed += Brake;
        brakeAction.canceled += Brake;

        escapeAction.performed += EscapeAcion;
        escapeAction.canceled += EscapeAcion;
    }

    public void UnsubscribeRoveActions()
    {
        // Remove subscriptions to prevent duplicate callbacks.
        if (movementAction == null || turnAction == null || brakeAction == null)
        {
            return;
        }

        movementAction.performed -= ForwardAndBackward;
        movementAction.canceled -= ForwardAndBackward;

        turnAction.performed -= Turn;
        turnAction.canceled -= Turn;

        brakeAction.performed -= Brake;
        brakeAction.canceled -= Brake;

        escapeAction.performed -= EscapeAcion;
        escapeAction.canceled -= EscapeAcion;

        // Reset the input values when disabled.
        movementInput = 0f;
        turnInput = 0f;
        brakeInput = false;
    }
}

