
using UnityEngine;

public class RoverBrain : MonoBehaviour
{
    [Header("References")]
    public RoverInput roverInput;
    public RoverMotor roverMotor;

    private void Update()
    {
        HandleMovement();
        HandleSteering();
        HandleBraking();
        roverMotor.UpdateWheelVisuals();
    }

    // Decides whether to move forward, backward, or stop applying motor torque.
    private void HandleMovement()
    {
        if (roverInput.movementInput > 0f)
        {
            roverMotor.GoForward();
        }
        else if (roverInput.movementInput < 0f)
        {
            roverMotor.GoBackward();
        }
        else
        {
            roverMotor.StopMotor();
        }
    }

    // Sends the steering input to the motor.
    private void HandleSteering()
    {
        roverMotor.TurnWheels(roverInput.turnInput);
    }

    // Applies or releases the brakes.
    private void HandleBraking()
    {
        if (roverInput.brakeInput)
        {
            roverMotor.Brake();
        }
        else
        {
            roverMotor.ReleaseBrake();
        }
    }
}

