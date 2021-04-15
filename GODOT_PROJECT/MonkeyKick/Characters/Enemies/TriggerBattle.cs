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
        private EnemyBattleInformation info;
        private string enemyParent = "EnemyOverworld";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            info = GetParent<EnemyBattleInformation>();
        }

        public void OnTriggerEnter(Node col)
        {
            if((string)col.GetMeta("Type") == "Player")
            {
                GD.Print("Collided with Player.");
                GameManager.ChangeGameState(GameStates.BATTLE);
                GameManager.battleList.Add(col);
                //GameManager.battleList.Add()
                GetTree().ChangeSceneTo(info.battleScene);
            }
        }
    }
}

