using System;
using UnityEngine;

public class InteractionBus : MonoBehaviour
{

    public static event EventHandler OnInteractionKeyPressed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnInteract()
    {
        if (OnInteractionKeyPressed != null) OnInteractionKeyPressed(this, EventArgs.Empty);
    }
}
