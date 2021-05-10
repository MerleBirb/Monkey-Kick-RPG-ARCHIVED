using Godot;
using System;

namespace Merlebirb.Managers
{
    //===== DEBUG CONTROLS =====//
    /*
    4/3/21
    Description: Debug controls wont be in the final game.

    */

    public class DebugControls : Node
    {
        [Export] private bool debugOn; // if TRUE, debug controls ON, if FALSE, debug controls OFF

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            if (debugOn)
            {
                GD.Print("Debug mode ON");
            }
            else
            {
                GD.Print("Debug mode OFF");
            }
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(float delta)
        {
            if (debugOn)
            {
                QuitGame();
            }
        }

        public void QuitGame()
        {
            if (Input.IsActionPressed("ui_cancel"))
            {
                GetTree().Quit();
            }
        }
    }
}
