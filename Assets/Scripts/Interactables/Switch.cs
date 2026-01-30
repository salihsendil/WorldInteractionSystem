using System;
using UnityEngine;

public class Switch : BaseInteractableToggle
{
    public event Action OnSwitchButtonPressed;

    public override void Interact()
    {
        OnSwitchButtonPressed?.Invoke();
        base.Interact();
    }
}
