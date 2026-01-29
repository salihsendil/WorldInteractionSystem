using UnityEngine;
using Zenject;

public class InteractionDetector : MonoBehaviour
{
    private InputHandler m_inputHandler;
    [SerializeField] private Camera m_playerCam;
    [SerializeField] private float m_rayRange = 2f;

    [Inject]
    private void Constructor(InputHandler inputHandler)
    {
        m_inputHandler = inputHandler;
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

    private void InteractionStarted()
    {
        IInteractable interactable = GetFindInteractable();
        interactable?.Interact();
    }

    private void InteractionCanceled()
    {
        IInteractable interactable = GetFindInteractable();
        interactable?.CancelInteract();
    }

    private IInteractable GetFindInteractable()
    {
        Ray ray = new Ray(m_playerCam.transform.position, m_playerCam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, m_rayRange))
        {
            if (hit.transform.gameObject.TryGetComponent(out IInteractable interactable))
            {
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
