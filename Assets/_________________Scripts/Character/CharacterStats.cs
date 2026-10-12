
using UnityEngine;

public class CharacterStats : MonoBehaviour
{


    [Header("Movement")]
    public float walkSpeed = 4f;
    public float sprintSpeed = 7f;
    public float jumpHeight = 1.5f;
    public float gravity = -20f;

    [Header("Mouse Look")]
    public Transform cameraPivot;
    public float mouseSensitivity = 0.1f;
    public float minPitch = -85f;
    public float maxPitch = 85f;






}