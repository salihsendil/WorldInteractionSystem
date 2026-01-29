using UnityEngine;
using Zenject;

public class PlayerLook : MonoBehaviour
{
    private InputHandler m_inputHandler;

    private float m_xRotation = 0f;
    [SerializeField] private float m_sensitivity = 0.1f;
    [SerializeField] private Transform m_playerCamera;

    [Inject]
    public void Construct(InputHandler inputHandler)
    {
        m_inputHandler = inputHandler;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (m_playerCamera == null)
            Debug.LogError("PlayerCamera is null. Please attach the player camera variable!");
    }

    private void LateUpdate()
    {
        HandleRotation();
    }

    public void HandleRotation()
    {
        Vector2 mouseDelta = m_inputHandler.MouseDelta;

        float mouseX = mouseDelta.x * m_sensitivity;
        float mouseY = mouseDelta.y * m_sensitivity;

        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);

        m_playerCamera.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}