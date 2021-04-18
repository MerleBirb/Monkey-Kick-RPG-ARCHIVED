using Godot;
using System;
using Merlebirb.TurnBasedSystem;
using Merlebirb.QualityOfLife;

//===== ENEMY BATTLE LOGIC =====//
/*
4/18/21
Description: handles the enemy's logic during battle.

*/

public class EnemyBattle : Spatial
{
    public TurnClass turnClass;
    [Export] public Character stats;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
