//===== CONTROL LISTENER =====//
/*
5/25/21
Description:
- Helps keep the controls consistent.

Author: lalo
*/

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlListener : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private string previousControlScheme;
    private string currentControlScheme;

    public Action<PlayerInput> onControlsChanged;

    private void Start()
    {
        Debug.Log("Initializing controls...");
        previousControlScheme = currentControlScheme = playerInput.currentControlScheme;
        onControlsChanged += NotifyControlsChange;
    }

    private void Update()
    {
        currentControlScheme = playerInput.currentControlScheme;

        if(currentControlScheme != previousControlScheme)
            onControlsChanged?.Invoke(playerInput);
                
        previousControlScheme = currentControlScheme;
    }

    private void NotifyControlsChange(PlayerInput playerInput)
    {
        Debug.Log(playerInput.currentControlScheme);
    }
}
