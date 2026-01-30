using UnityEngine;
using Zenject;

public class Door : BaseInteractableToggle
{
    private SignalBus m_signalBus;
    [SerializeField] private Switch m_remoteController;
    [SerializeField] private InventoryItemSO m_requiredItem;
    [SerializeField] private int m_requiredKeyAmount = 1;
    [SerializeField] private bool m_isLocked = true;


    [Inject]
    private void Constructor(SignalBus signalBus)
    {
        m_signalBus = signalBus;
    }

    private void OnEnable()
    {
        if (m_remoteController != null)
            m_remoteController.OnSwitchButtonPressed += RemoteControl;

    }

    private void OnDisable()
    {
        if (m_remoteController != null)
            m_remoteController.OnSwitchButtonPressed -= RemoteControl;
    }

    private void RemoteControl()
    {
        m_isLocked = false;
        Interact();
    }

    public override void Interact()
    {
        if (m_isLocked)
        {
            if (m_inventory.HasItem(m_requiredItem, m_requiredKeyAmount))
            {
                m_isLocked = false;
                base.Interact();
                m_signalBus.Fire(new OnPrompTextChangedSignal("Door is open"));
                return;
            }
            m_signalBus.Fire(new OnPrompTextChangedSignal("Door is locked"));
            return;
        }
        Debug.Log("No need to key for open/close");
        base.Interact();
    }
}
