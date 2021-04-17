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
        //private GameManager gameManager;
        private TurnSystem turnSystem;
        private EnemyBattleInformation info;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            turnSystem = (TurnSystem)GetNode<TurnSystem>("/root/TurnSystem");
            if (turnSystem != null) 
            { 
                GD.Print("Turn system imported"); 
            }
            else
            {
                GD.Print("Error: no turn system");
            }

            //gameManager = (GameManager)GetNode<GameManager>("/root/GameManager");
            //if (gameManager != null) { GD.Print("Game Manager imported"); };

            info = GetParent<EnemyBattleInformation>();
        }

        public void OnTriggerEnter(Node col)
        {
            if((string)col.GetMeta("Type") == "Player")
            {
                GD.Print("Collided with Player.");
                GameManager.ChangeGameState(GameStates.BATTLE);
                //turnSystem.StartBattle();
                GetTree().ChangeSceneTo(info.battleScene);
            }
        }
    }
}

