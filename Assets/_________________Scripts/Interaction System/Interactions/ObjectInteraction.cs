using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class ObjectInteraction : MonoBehaviour, iInteractable
{
    [SerializeField] private Transform objectPlaceHolder;
    private Rigidbody rb;

    private void Start()
    {
        objectPlaceHolder = GameObject.FindGameObjectWithTag("Object Place Holder").GetComponent<Transform>(); 
        rb = GetComponent<Rigidbody>();
    }
    public bool CarInteract()
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        MoveOject();
        return true;
    }
    public void StopInteract()
    {
        rb.isKinematic = false;
    }

    private void MoveOject()
    {
        if(objectPlaceHolder == null)
        {
            return;
        }
        transform.position = objectPlaceHolder.position;
        rb.isKinematic = true;
    }

    
}
