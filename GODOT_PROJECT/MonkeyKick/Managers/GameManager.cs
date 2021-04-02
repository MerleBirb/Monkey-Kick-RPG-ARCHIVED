using Godot;
using System;

namespace Merlebirb.Managers
{
    //===== GAME MANAGER =====//
    /*
    4/1/21 (april fools lol trolled)
    Description: An extremely important script that controls aspects of the game's state and holds very important variables.

    */

    public class GameManager : Node
    {
        public enum GameStates // various states the game can be in, might add more depending on what is in the game.
        {
            MAIN_MENU = 100,
            OVERWORLD = 200,
            BATTLE = 300,
            CUTSCENE = 400,
            PAUSED = 500
        }

        public static GameStates state;

        public override void _Ready()
        {
            state = GameStates.MAIN_MENU;
        }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

        public static void ChangeGameState(GameStates newState)
        {
            if (state == newState) { return; };
            state = newState;
        }
    }

}
