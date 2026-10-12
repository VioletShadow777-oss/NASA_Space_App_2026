
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterInput))]
[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(CharacterStats))]
public class CharacterBrain : MonoBehaviour
{
    private CharacterController _controller;
    private CharacterInput _characterInput;
    private CharacterMotor _characterMotor;
    private CharacterStats _characterStats;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _characterInput = GetComponent<CharacterInput>();
        _characterMotor = GetComponent<CharacterMotor>();
        _characterStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        // Update stats and determine sprint availability.
        bool isSprinting = _characterInput.SprintHeld;

        // Consume jump input once per press.
        bool jumpPressed =
            _characterInput.ConsumeJumpPressed();

        // Apply character movement and gravity.
        _characterMotor.HandleMovement(
            _characterInput.MoveInput,
            isSprinting,
            jumpPressed,
            deltaTime);

        // Apply horizontal and vertical camera look.
        _characterMotor.HandleLook(
            _characterInput.LookInput);
    }
}