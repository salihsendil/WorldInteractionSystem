using UnityEngine;
using Zenject;

public abstract class BaseInteractableToggle : MonoBehaviour, IInteractable
{
    private SignalBus m_signalBus;
    protected bool m_isOpen = false;
    protected Inventory m_inventory;

    [Inject]
    private void Constructor(SignalBus signalBus, Inventory inventory)
    {
        m_inventory = inventory;
        m_signalBus = signalBus;
    }

    public virtual void Interact()
    {
        m_isOpen = !m_isOpen;
        if (m_isOpen)
        {
            Debug.Log("Door is open");
            return;
        }
        Debug.Log("Door is closed");
    }

    public void CancelInteract() { }

    public void InteractMessage()
    {
        m_signalBus.Fire(new OnPrompTextChangedSignal("Press 'E' for use"));
    }
}
