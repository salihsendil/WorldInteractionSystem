using UnityEngine;

public abstract class BaseInteractableHold : MonoBehaviour, IInteractable
{
    private Timer timer;
    [SerializeField] private float openingDuration = 3f;
    protected bool m_hasOpened = false;

    private void Start()
    {
        timer = new();
    }

    public virtual void Interact()
    {
        if (m_hasOpened)
            return;
        timer.SetTimer(openingDuration);
    }

    private void Update()
    {
        if (timer.IsFinished())
        {

        }
    }

    public void CancelInteract()
    {

    }
}
