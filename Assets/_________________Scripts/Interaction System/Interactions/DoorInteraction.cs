using DG.Tweening;
using UnityEngine;

public class DoorInteraction : MonoBehaviour, iInteractable
{
    private BoxCollider doorCollider;
    private Animator doorAnimator;
    private bool _isOpen = false;

    private void Start()
    {
        doorCollider = GetComponent<BoxCollider>();
        doorAnimator = GetComponent<Animator>();
    }
    public bool CarInteract()
    {
        return true; // Because the door always can be interacted
    }

    public bool Interact(Interactor interactor)
    {
        // what happens when Interact
        doorAnimator.SetTrigger("doorOpen_t");
        return true;
    }

    public void DisableCollider()
    {
        doorCollider.enabled = false;
    }

    public void EnableCollider()
    {
        doorCollider.enabled = true;
    }
}
