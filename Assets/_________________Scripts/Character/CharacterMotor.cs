
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMotor : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField, Min(0f)] private float walkSpeed = 4f;
    [SerializeField, Min(0f)] private float sprintSpeed = 7f;
    [SerializeField, Min(0f)] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -20f;

    [Header("Mouse Look")]
    [SerializeField] private Transform cameraPivot;
    [SerializeField, Min(0f)] private float mouseSensitivity = 0.1f;
    [SerializeField] private float minPitch = -85f;
    [SerializeField] private float maxPitch = 85f;

    private CharacterController _controller;
    private float _verticalVelocity;
    private float _pitch;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        if (cameraPivot != null)
        {
            _pitch = cameraPivot.localEulerAngles.x;

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

        float speed = isSprinting ? sprintSpeed : walkSpeed;

        // Maintain a small downward velocity while grounded.
        if (_controller.isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -2f;
        }

        if (jumpPressed && _controller.isGrounded)
        {
            _verticalVelocity =
                Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        _verticalVelocity += gravity * deltaTime;

        Vector3 velocity =
            moveDirection * speed +
            Vector3.up * _verticalVelocity;

        _controller.Move(velocity * deltaTime);
    }

    public void HandleLook(Vector2 lookInput)
    {
        // Mouse delta is already a frame-based pointer displacement.
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        // Rotate the character horizontally.
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically.
        if (cameraPivot == null)
            return;

        _pitch -= mouseY;
        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);

        cameraPivot.localRotation =
            Quaternion.Euler(_pitch, 0f, 0f);
    }
}