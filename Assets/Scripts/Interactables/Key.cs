using UnityEngine;

public class Key : BaseInteractableInstant
{
    public override void Interact()
    {
        Debug.Log(name + " collected!");
        Destroy(gameObject);
    }
}
