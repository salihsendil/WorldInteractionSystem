using UnityEngine;
using Zenject;

public abstract class BaseInteractableHold : MonoBehaviour, IInteractable
{
    private SignalBus m_signalBus;
    private Timer m_timer;
    [SerializeField] private float m_openingDuration = 3f;
    protected bool m_hasOpened = false;
    protected bool m_isOpening = false;

    [Inject]
    private void Constructor(SignalBus signalBus)
    {
        m_signalBus = signalBus;
    }

    private void Start()
    {
        m_timer = new();
    }

    public virtual void Interact()
    {
        if (m_hasOpened)
            return;
        m_timer.SetTimer(m_openingDuration);
        m_isOpening = true;
    }

    private void Update()
    {
        if (m_isOpening && !m_timer.IsFinished())
        {
            m_timer.TickTimer();
            m_signalBus.Fire(new OnProgressBarChangedSignal(m_timer.Duration, m_timer.Remaining));
            if (m_timer.IsFinished())
            {
                InteractComplete();
            }
        }
    }

    public void CancelInteract()
    {
        m_isOpening = false;
    }

    protected virtual void InteractComplete()
    {
        m_hasOpened = true;
    }
}
