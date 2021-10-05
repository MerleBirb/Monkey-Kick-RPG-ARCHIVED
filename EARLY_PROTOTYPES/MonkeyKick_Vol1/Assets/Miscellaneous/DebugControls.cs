//=====  DEBUG CONTROLS =====//
/*
5/23/21
Description:
- Just some extra controls.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class DebugControls : MonoBehaviour
{

    public bool isDebugModeOn = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!isDebugModeOn)
        {
            Cursor.visible = false;
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDebugModeOn)
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame) { Application.Quit(); }
        }
    }
}
