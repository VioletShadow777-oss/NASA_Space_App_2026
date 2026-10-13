
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CharacterInput : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputActionMap _playerActionMap;
    private Interactor interactor;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    public bool JumpPressed { get; private set; }
    public bool SprintHeld { get; private set; }


    private bool canLook = true; // used to define whether player can look with the mouse or can navigate in menues
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        interactor = GetComponentInChildren<Interactor>();

        _playerActionMap = _playerInput.actions.FindActionMap("Player", true);
    }

    private void OnEnable()
    {
        SubscribePlayerActions();
    }

    private void OnDisable()
    {
        UnsubscribePlayerAction();
    }

    public void Move(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
            JumpPressed = true;

        if (context.canceled)
            JumpPressed = false;
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        SprintHeld = context.ReadValue<float>() > 0.5f;
    }

    public void TabActios(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UIManager.Instance.PlayerMenuToggle();
            CursorManager.Instance.ToggelCursor();
            //ToggelMouseControl();
        }
    }

    // Called by CharacterBrain to consume a jump press once.
    public bool ConsumeJumpPressed()
    {
        if (!JumpPressed)
            return false;

        JumpPressed = false;
        return true;
    }

    private void ToggelMouseControl()
    {
        if (!canLook)
        {
            // Enable look here
            _playerActionMap.FindAction("Look", true).performed += Look;
            _playerActionMap.FindAction("Look", true).canceled += Look;
            canLook = true;
        }
        else
        {
            // Disable Look here
            _playerActionMap.FindAction("Look", true).performed -= Look;
            _playerActionMap.FindAction("Look", true).canceled -= Look;
            canLook = false;
        }
    }

    public void SubscribePlayerActions()
    {
        if (_playerActionMap == null)
            return;

        _playerActionMap.FindAction("Move", true).performed += Move;
        _playerActionMap.FindAction("Move", true).canceled += Move;

        _playerActionMap.FindAction("Look", true).performed += Look;
        _playerActionMap.FindAction("Look", true).canceled += Look;

        _playerActionMap.FindAction("Jump", true).performed += Jump;
        _playerActionMap.FindAction("Jump", true).canceled += Jump;

        _playerActionMap.FindAction("Sprint", true).performed += Sprint;
        _playerActionMap.FindAction("Sprint", true).canceled += Sprint;

        _playerActionMap.FindAction("Tab Actions", true).performed += TabActios;
        _playerActionMap.FindAction("Tab Actions", true).canceled += TabActios;

        _playerActionMap.FindAction("Interaction", true).performed += interactor.InteractionInput;
        _playerActionMap.FindAction("Interaction", true).canceled += interactor.InteractionInput;
    }

    public void UnsubscribePlayerAction()
    {
        if (_playerActionMap == null)
            return;

        _playerActionMap.FindAction("Move").performed -= Move;
        _playerActionMap.FindAction("Move").canceled -= Move;

        _playerActionMap.FindAction("Look").performed -= Look;
        _playerActionMap.FindAction("Look").canceled -= Look;

        _playerActionMap.FindAction("Jump").performed -= Jump;
        _playerActionMap.FindAction("Jump").canceled -= Jump;

        _playerActionMap.FindAction("Sprint").performed -= Sprint;
        _playerActionMap.FindAction("Sprint").canceled -= Sprint;

        _playerActionMap.FindAction("Tab Actions", true).performed -= TabActios;
        _playerActionMap.FindAction("Tab Actions", true).canceled -= TabActios;

        _playerActionMap.FindAction("Interaction", true).performed -= interactor.InteractionInput;
        _playerActionMap.FindAction("Interaction", true).canceled -= interactor.InteractionInput;
    }
}