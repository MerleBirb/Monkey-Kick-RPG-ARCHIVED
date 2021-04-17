using Godot;
using System;
using Merlebirb.Managers;
using Merlebirb.QualityOfLife;

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
            info = GetParent<EnemyBattleInformation>();

            if (info == null)
            {
                GD.PrintErr("Error: the enemy battle info was unable to be loaded.");
            }
            else
            {
                GD.Print("Loaded enemy battle info.");
            }
        }

        public void OnTriggerEnter(Node col)
        {
            if((string)col.GetMeta(ObjectTags.TAG) == ObjectTags.PLAYER)
            {
                GD.Print("Collided with Player.");
                GameManager.ChangeGameState(GameStates.BATTLE);
                TurnSystem.StartBattle(info.enemyParty);
                GetTree().ChangeSceneTo(info.battleScene);
            }
        }
    }
}

