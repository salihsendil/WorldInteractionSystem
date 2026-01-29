using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInputMap m_inputActions;
    private Vector2 m_inputVector;
    private Vector3 m_movementVector;

    private Vector2 m_mouseDeltaInput;

    public Vector3 MovementVector => m_movementVector;
    public Vector2 MouseDelta => m_mouseDeltaInput;

    public event Action OnInteractionButtonPressed;

    private void Awake()
    {
        m_inputActions = new PlayerInputMap();
    }

    private void OnEnable()
    {
        m_inputActions.Enable();
        m_inputActions.Movement.Move.started += Move;
        m_inputActions.Movement.Move.performed += Move;
        m_inputActions.Movement.Move.canceled += Move;

        m_inputActions.Look.CamRotation.started += PlayerLook;
        m_inputActions.Look.CamRotation.performed += PlayerLook;
        m_inputActions.Look.CamRotation.canceled += PlayerLook;

        m_inputActions.Interaction.E_Interact.performed += Interaction;
    }

    private void OnDisable()
    {
        m_inputActions.Disable();
        m_inputActions.Movement.Move.started -= Move;
        m_inputActions.Movement.Move.performed -= Move;
        m_inputActions.Movement.Move.canceled -= Move;

        m_inputActions.Look.CamRotation.started -= PlayerLook;
        m_inputActions.Look.CamRotation.performed -= PlayerLook;
        m_inputActions.Look.CamRotation.canceled -= PlayerLook;

        m_inputActions.Interaction.E_Interact.performed -= Interaction;
    }

    private void Move(InputAction.CallbackContext callback)
    {
        m_inputVector = callback.ReadValue<Vector2>();
        m_movementVector = ConvertInputToMovementVector(m_inputVector);
    }

    private Vector3 ConvertInputToMovementVector(Vector2 input)
    {
        return new Vector3(input.x, 0f, input.y);
    }

    private void PlayerLook(InputAction.CallbackContext callback)
    {
        m_mouseDeltaInput = callback.ReadValue<Vector2>();
    }

    private void Interaction(InputAction.CallbackContext callback)
    {
        OnInteractionButtonPressed?.Invoke();
    }
}
