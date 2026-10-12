using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public float _castDistance = 5f;


    public void InteractionInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(DoInteractionTest(out iInteractable interactable))
            {
                if (interactable.CarInteract())
                {
                    interactable.Interact(this);
                }
            }
        }
    }
    private bool DoInteractionTest(out iInteractable interactable)
    {
        interactable = null;

        Ray ray = new Ray(transform.position, transform.forward);

        if(Physics.Raycast(ray, out RaycastHit hit, _castDistance))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
            interactable = hit.collider.GetComponent<iInteractable>();
            if(interactable != null)
            {
                return true;
            }

            return false;
        }

        Debug.DrawRay(transform.position, transform.forward * _castDistance, Color.green);


        return false;
    }
}
