using UnityEngine;

public class Door : BaseInteractableToggle
{
    private bool m_isLocked = true;

    public override void Interact()
    {
        if (m_isLocked)
        {
            Debug.LogError("Door is Locked!");
            return;
        }
        base.Interact();
    }
}
