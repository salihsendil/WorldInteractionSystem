using UnityEngine;
using Zenject;

public abstract class BaseInteractableToggle : MonoBehaviour, IInteractable
{
    protected Inventory m_inventory;
    protected bool m_isOpen = false;

    [Inject]
    private void Constructor(Inventory inventory)
    {
        m_inventory = inventory;
    }

    public virtual void Interact()
    {
        m_isOpen = !m_isOpen;
    }

    public void CancelInteract() { }
}
