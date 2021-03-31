using Godot;
using System;

namespace Merlebirb.Scenes
{
    //===== START NEW SCENE =====//
    /*
    3/31/21
    Description: Starts a new game of Monkey Kick.

    */

    public class StartNewGame : Button
    {
        [Export] private string sceneName = "";

        private void _on_NewGameButton_pressed()
        {
            GetTree().ChangeScene(sceneName);
        }
    }
}

