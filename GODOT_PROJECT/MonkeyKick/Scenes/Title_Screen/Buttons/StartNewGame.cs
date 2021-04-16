using Godot;
using System;
using Merlebirb.Managers;

namespace Merlebirb.Scenes
{
    //===== START NEW SCENE =====//
    /*
    3/31/21
    Description: Starts a new game of Monkey Kick.

    */

    public class StartNewGame : Button
    {
        [Export] private PackedScene scene;

        private void _on_NewGameButton_pressed()
        {
            GetTree().ChangeSceneTo(scene);
            GameManager.ChangeGameState(GameStates.OVERWORLD);
        }
    }
}

