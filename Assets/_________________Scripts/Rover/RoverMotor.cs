
using UnityEngine;

public class RoverMotor : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;

    public WheelCollider middleLeftWheel;
    public WheelCollider middleRightWheel;

    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    [Header ("Wheel Meshes")]
    public Transform frontLeftWheelMesh;
    public Transform frontRightWheelMesh;

    public Transform middleLeftWheelMesh;
    public Transform middleRightWheelMesh;

    public Transform rearLeftWheelMesh;
    public Transform rearRightWheelMesh;


    [Header("Motor Settings")]
    public float motorTorque = 1200f;

    [Header("Steering Settings")]
    public float steeringAngle = 25f;

    [Header("Brake Settings")]
    public float brakeTorque = 3000f;

    // Applies forward motor torque to all six wheels.
    public void GoForward()
    {
        ApplyMotorTorque(motorTorque);
    }

    // Applies reverse motor torque to all six wheels.
    public void GoBackward()
    {
        ApplyMotorTorque(-motorTorque);
    }

    // Applies motor torque to every wheel.
    private void ApplyMotorTorque(float torque)
    {
        frontLeftWheel.motorTorque = torque;
        frontRightWheel.motorTorque = torque;

        middleLeftWheel.motorTorque = torque;
        middleRightWheel.motorTorque = torque;

        rearLeftWheel.motorTorque = torque;
        rearRightWheel.motorTorque = torque;
    }

    // Steers the front and rear axles in opposite directions.
    // Input: -1 = left, 1 = right.
    public void TurnWheels(float input)
    {
        float angle = input * steeringAngle;

        // Front axle turns normally.
        frontLeftWheel.steerAngle = angle;
        frontRightWheel.steerAngle = angle;

        // Middle axle does not steer.
        middleLeftWheel.steerAngle = 0f;
        middleRightWheel.steerAngle = 0f;

        // Rear axle turns in the opposite direction.
        rearLeftWheel.steerAngle = -angle;
        rearRightWheel.steerAngle = -angle;
    }

    // Applies brakes to all six wheels.
    public void Brake()
    {
        ApplyBrakeTorque(brakeTorque);
    }

    // Removes braking force from all six wheels.
    public void ReleaseBrake()
    {
        ApplyBrakeTorque(0f);
    }

    private void ApplyBrakeTorque(float torque)
    {
        frontLeftWheel.brakeTorque = torque;
        frontRightWheel.brakeTorque = torque;

        middleLeftWheel.brakeTorque = torque;
        middleRightWheel.brakeTorque = torque;

        rearLeftWheel.brakeTorque = torque;
        rearRightWheel.brakeTorque = torque;
    }

    // Removes motor force when the rover is not moving forward or backward.
    public void StopMotor()
    {
        ApplyMotorTorque(0f);
    }

    public void UpdateWheelVisuals()
    {
        BindWheelColliderAndMesh(frontLeftWheel, frontLeftWheelMesh);
        BindWheelColliderAndMesh(frontRightWheel, frontRightWheelMesh);

        BindWheelColliderAndMesh(middleLeftWheel, middleLeftWheelMesh);
        BindWheelColliderAndMesh(middleRightWheel, middleRightWheelMesh);

        BindWheelColliderAndMesh(rearLeftWheel, rearLeftWheelMesh);
        BindWheelColliderAndMesh(rearRightWheel, rearRightWheelMesh);
    }

    private void BindWheelColliderAndMesh(WheelCollider targetWheelCollider, Transform visualWheelMesh)
    {
        // Get the calculated physics position and rotation from the Wheel Collider
        targetWheelCollider.GetWorldPose(out Vector3 worldPosition, out Quaternion worldRotation);

        // Apply those values to the visual mesh transform
        visualWheelMesh.position = worldPosition;
        visualWheelMesh.rotation = worldRotation;
    }
}

