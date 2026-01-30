using UnityEngine;
using Zenject;

public class InteractionDetector : MonoBehaviour
{
    private InputHandler m_inputHandler;
    private SignalBus m_signalBus;
    [SerializeField] private Camera m_playerCam;
    [SerializeField] private float m_rayRange = 2f;

    [Inject]
    private void Constructor(InputHandler inputHandler, SignalBus signalBus)
    {
        m_inputHandler = inputHandler;
        m_signalBus = signalBus;
    }


    private void OnEnable()
    {
        m_inputHandler.OnInteractStarted += InteractionStarted;
        m_inputHandler.OnInteractCanceled += InteractionCanceled;
    }

    private void OnDisable()
    {
        m_inputHandler.OnInteractStarted -= InteractionStarted;
        m_inputHandler.OnInteractCanceled -= InteractionCanceled;
    }

    private void Update()
    {
        GetFindInteractable();
    }

    private void InteractionStarted()
    {
        IInteractable interactable = GetFindInteractable();
        if (interactable == null)
        {
            m_signalBus.Fire(new OnPrompTextChangedSignal("No object for interact"));
        }
        else
        {
            interactable.Interact();
        }
    }

    private void InteractionCanceled()
    {
        IInteractable interactable = GetFindInteractable();
        if (interactable == null)
        {
            m_signalBus.Fire(new OnPrompTextChangedSignal("No object for interact"));
        }
        else
        {
            interactable.CancelInteract();
        }
    }

    private IInteractable GetFindInteractable()
    {
        Ray ray = new Ray(m_playerCam.transform.position, m_playerCam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, m_rayRange))
        {
            if (hit.transform.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.InteractMessage();
                return interactable;
            }
        }
        return null;
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        Ray ray = new Ray(m_playerCam.transform.position, m_playerCam.transform.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }
    #endregion
}
