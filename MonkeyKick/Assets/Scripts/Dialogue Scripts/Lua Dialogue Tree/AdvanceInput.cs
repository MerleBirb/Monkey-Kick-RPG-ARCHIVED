using UnityEngine;
using System.Collections;

public class AdvanceInput : MonoBehaviour
{
    ////////// LUA INPUTS //////////
    /// this is the script that takes all the inputs for scrolling dialogue and all that jazz

    // store the lua environment
    private LuaEnvironment lua;

    // store the button handler
    private ButtonHandler buttons;

    // Start is called on the first frame
    private void Start()
    {
        lua = FindObjectOfType<LuaEnvironment>();
        buttons = FindObjectOfType<ButtonHandler>();
    }
}
