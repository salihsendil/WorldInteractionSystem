using UnityEngine;

public class Door : BaseInteractableToggle
{
    [SerializeField] private InventoryItemSO m_requiredItem;
    [SerializeField] private int m_requiredKeyAmount = 1;
    private bool m_isLocked = true;

    public override void Interact()
    {
        if (m_isLocked)
        {
            if (m_inventory.HasItem(m_requiredItem, m_requiredKeyAmount))
            {
                m_isLocked = false;
                base.Interact();
                Debug.Log("Door is Opened!");
                return;
            }
            Debug.LogError("Door is Locked!");
            return;
        }
        Debug.Log("No need to key for open/close");
        base.Interact();
    }
}
