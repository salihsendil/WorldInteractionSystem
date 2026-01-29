using UnityEngine;

public abstract class BaseInteractableInstant : MonoBehaviour, IInteractable
{
    public abstract void Interact();
    public void CancelInteract() { }
}
