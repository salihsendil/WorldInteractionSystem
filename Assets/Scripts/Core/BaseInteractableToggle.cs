using UnityEngine;

public abstract class BaseInteractableToggle : MonoBehaviour, IInteractable
{
    protected bool m_isOpen = false;

    public virtual void Interact()
    {
        m_isOpen = !m_isOpen;
    }

    public void CancelInteract() { }
}
