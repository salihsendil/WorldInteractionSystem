using UnityEngine;
using Zenject;

public abstract class BaseInteractableInstant : MonoBehaviour, IInteractable
{
    private SignalBus m_signalBus;

    [Inject]
    private void Constructor(SignalBus signalBus)
    {
        m_signalBus = signalBus;
    }

    public abstract void Interact();
    public void CancelInteract() { }

    public void InteractMessage()
    {
        m_signalBus.Fire(new OnPrompTextChangedSignal("Press 'E' for collect"));
    }
}
