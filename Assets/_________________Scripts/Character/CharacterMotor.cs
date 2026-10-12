
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMotor : MonoBehaviour
{
  

    private CharacterController _controller;
    private CharacterStats characterStats;
    private float _verticalVelocity;
    private float _pitch;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        characterStats = GetComponent<CharacterStats>();

        if (characterStats.cameraPivot != null)
        {
            _pitch = characterStats.cameraPivot.localEulerAngles.x;

            if (_pitch > 180f)
                _pitch -= 360f;
        }
    }

    public void HandleMovement(
        Vector2 moveInput,
        bool isSprinting,
        bool jumpPressed,
        float deltaTime)
    {
        // Keep movement relative to the player's facing direction.
        Vector3 moveDirection =
            transform.right * moveInput.x +
            transform.forward * moveInput.y;

        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        float speed = isSprinting ? characterStats.sprintSpeed : characterStats.walkSpeed;

        // Maintain a small downward velocity while grounded.
        if (_controller.isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -2f;
        }

        if (jumpPressed && _controller.isGrounded)
        {
            _verticalVelocity =
                Mathf.Sqrt(characterStats.jumpHeight * -2f * characterStats.gravity);
        }

        _verticalVelocity += characterStats.gravity * deltaTime;

        Vector3 velocity =
            moveDirection * speed +
            Vector3.up * _verticalVelocity;

        _controller.Move(velocity * deltaTime);
    }

    public void HandleLook(Vector2 lookInput)
    {
        // Mouse delta is already a frame-based pointer displacement.
        float mouseX = lookInput.x * characterStats.mouseSensitivity;
        float mouseY = lookInput.y * characterStats.mouseSensitivity;

        // Rotate the character horizontally.
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically.
        if (characterStats.cameraPivot == null)
            return;

        _pitch -= mouseY;
        _pitch = Mathf.Clamp(_pitch, characterStats.minPitch, characterStats.maxPitch);

        characterStats.cameraPivot.localRotation =
            Quaternion.Euler(_pitch, 0f, 0f);
    }
}