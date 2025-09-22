
using System.Collections.Generic;
using UnityEngine;

public abstract class InputHandler
{
    protected Dictionary<KeyCode, IKeyAction> keyActions;

    public abstract void Initialize();

    public void HandleInput()
    {
        foreach (var keyAction in keyActions)
        {
            if (Input.GetKeyDown(keyAction.Key))
            {
                keyAction.Value.Execute();
            }
        }
    }
}