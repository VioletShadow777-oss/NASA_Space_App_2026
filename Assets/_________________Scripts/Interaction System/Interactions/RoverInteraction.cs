using UnityEngine;
using UnityEngine.InputSystem;

public class RoverInteraction : MonoBehaviour, iInteractable
{
    public CharacterInput characterInput;
    public RoverInput roverInput;
    public PlayerInput playerInput;

    public Camera playerCamera;
    public Camera roverCamera;

    public Transform playerEnteredPosition;
    public Transform playerExitPosition;
    public Transform player;
    private CharacterController playerCharacterController;
    private CharacterBrain characterBrain;



    private void Start()
    {
        playerCharacterController = player.GetComponent<CharacterController>();
        characterBrain = player.GetComponent<CharacterBrain>();
    }
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
        playerInput.SwitchCurrentActionMap("Rover");
        characterInput.UnsubscribePlayerAction();
        roverInput.SubscribeRoverActions();

        // setting priority
        roverCamera.depth = 1;
        playerCamera.depth = 0;

        // set the player inside the vehicle
        player.SetParent(playerEnteredPosition);

        characterBrain.enabled = false;
        playerCharacterController.enabled = false;
        player.localPosition = new Vector3(0, 0, 0);
        
    }

    public void ExitRover()
    {
        playerInput.SwitchCurrentActionMap("Player");
        characterInput.SubscribePlayerActions();
        roverInput.UnsubscribeRoveActions();

        // setting priority
        roverCamera.depth = 0;
        playerCamera.depth = 1;

        // Kick the player out from the vehicle
        player.parent = null;

        playerCharacterController.enabled = false;
        player.localPosition = playerExitPosition.position;
        player.rotation = Quaternion.Euler(0, 0, 0);
        playerCharacterController.enabled = true;
        characterBrain.enabled = true;
       
    }
}
