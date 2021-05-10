using Godot;
using System;
using System.Collections.Generic;
using Merlebirb.QualityOfLife;

//===== ENEMY BATTLE INFORMATION =====//
/*
4/15/21
Description: stores the information to be loaded into an enemy when battle is loaded.

*/

public class EnemyBattleInformation : KinematicBody
{
    [Export] public PackedScene battleScene; // save the path of the battle scene here.
    [Export] public List<PackedScene> enemiesToSpawnScenes = new List<PackedScene>();
    public List<Node> enemiesToSpawnList = new List<Node>();
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        EnemyListToEnemyParty();
    }

    private void EnemyListToEnemyParty()
    {
        if (enemiesToSpawnScenes.Count == 0)
        {
            GD.PrintErr("Error: there are no enemies to spawn for battle.");
            return;
        }
        else
        {
            for (int i = 0; i < enemiesToSpawnScenes.Count; i++)
            {
                Node enemyInstance = enemiesToSpawnScenes[i].Instance();
                enemiesToSpawnList.Add(enemyInstance);
                GD.Print("Added " + enemyInstance.Name + " to the Enemy Party.");
            }
        }
    }
}
