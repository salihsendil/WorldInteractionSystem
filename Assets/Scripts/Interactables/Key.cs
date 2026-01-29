using UnityEngine;
using Zenject;

public class Key : BaseInteractableInstant
{
    private SignalBus m_signalBus;
    [SerializeField] private InventoryItemSO m_itemInventoryData;
    [SerializeField] private int m_quantity;

    [Inject]
    private void Constructor(SignalBus signalBus)
    {
        m_signalBus = signalBus;
    }

    public override void Interact()
    {
        Debug.Log(name + " collected!");
        m_signalBus.Fire(new OnInventoryItemCollectedSignal(m_itemInventoryData, m_quantity));
        Destroy(gameObject);
    }
}
