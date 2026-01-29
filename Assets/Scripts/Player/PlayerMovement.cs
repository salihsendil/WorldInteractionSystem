using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    private InputHandler m_inputHandler;

    [SerializeField] private float m_speed = 4f;

    [Inject]
    private void Constructor(InputHandler inputHandler)
    {
        m_inputHandler = inputHandler;
    }

    private void Update()
    {
        if (m_inputHandler.MovementVector != Vector3.zero)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        transform.Translate(m_inputHandler.MovementVector * m_speed * Time.deltaTime);
    }
}
