using UnityEngine;
using UnityEngine.InputSystem;

public class RoverInteraction : MonoBehaviour, iInteractable
{
    public CharacterInput characterInput;
    public RoverInput roverInput;
    public PlayerInput playerInput;
    public bool CarInteract()
    {
        return true; // Because Always can be Interacted
    }

    public bool Interact(Interactor interactor)
    {
        // What happes When Interact
        EnterRover();
        return true;


    }

    public void EnterRover()
    {
        characterInput.UnsubscribePlayerAction();
        roverInput.SubscribeRoverActions();
        playerInput.SwitchCurrentActionMap("Rover");
    }

    public void ExitRover()
    {
        characterInput.SubscribePlayerActions();
        roverInput.UnsubscribeRoveActions();
        playerInput.SwitchCurrentActionMap("Player");
    }
}
