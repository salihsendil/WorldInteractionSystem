using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour
{
    private SignalBus m_signalBus;
    [SerializeField] private InteractionPromptController m_promptController;
    [SerializeField] private ProgressBarController m_progressBar;

    [Inject]
    private void Constructor(SignalBus signalBus)
    {
        m_signalBus = signalBus;
    }

    private void OnEnable()
    {
        m_signalBus.Subscribe<OnProgressBarChangedSignal>(OnProgressBarHandleChanged);
    }

    private void OnDisable()
    {
        m_signalBus.Unsubscribe<OnProgressBarChangedSignal>(OnProgressBarHandleChanged);
    }

    private void OnPromptTextChanged()
    {

    }

    private void OnProgressBarHandleChanged(OnProgressBarChangedSignal signal)
    {
        m_progressBar.Set(signal.Duration);
        m_progressBar.UpdateProgress(signal.Remaining);
    }

}
