using Godot;
using System;
using Merlebirb.Managers;

namespace Merlebirb.TurnBasedSystem
{
    //===== TRIGGER BATTLE =====//
    /*
    4/1/21
    Description: Detects if the player has entered the trigger radius of the enemy to trigger the battle scene.

    */

    public class TriggerBattle : Area
    {
        [Export] private string battleScene = ""; // save the path of the battle scene here.

        public void OnTriggerEnter(Node col)
        {
            if((string)col.GetMeta("Type") == "Player")
            {
                GD.Print("Collided with Player.");
                GameManager.ChangeGameState(GameStates.BATTLE);
                GetTree().ChangeScene(battleScene);
            }
        }
    }
}

