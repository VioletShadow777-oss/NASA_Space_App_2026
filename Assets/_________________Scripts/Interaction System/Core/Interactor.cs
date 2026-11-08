using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public float _castDistance = 5f;
    private iInteractable currentInteractable;
    private float interactionInput;
    private void Update()
    {
        if (DoInteractionTest(out iInteractable interactable))
        {
            if (interactable.CarInteract())
            {
                UIManager.Instance.ShowInteractionUI(); // Show interaction UI if it can be Interacted
                if (interactionInput == 1)
                {
                    currentInteractable = interactable;
                    interactable.Interact(this);

                }
                else
                {
                    StopInteraction();
                }
                    return;
            }



        }
        UIManager.Instance.HideInteractionUI(); // Hide interaction UI when it cannot be intercated
    }

    public void InteractionInput(InputAction.CallbackContext context)
    {

        interactionInput = context.ReadValue<float>();

    }
    private bool DoInteractionTest(out iInteractable interactable)
    {
        interactable = null;

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _castDistance))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
            interactable = hit.collider.GetComponent<iInteractable>();
            if (interactable != null)
            {
                return true;
            }

            return false;
        }

        Debug.DrawRay(transform.position, transform.forward * _castDistance, Color.green);


        return false;
    }
    private void StopInteraction()
    {
        if(currentInteractable == null)
        {
            return;
        }

        currentInteractable.StopInteract();
        currentInteractable = null;
    }
}
