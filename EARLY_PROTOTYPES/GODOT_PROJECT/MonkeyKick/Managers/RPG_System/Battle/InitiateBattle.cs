using Godot;
using System;
using Merlebirb.TurnBasedSystem;

//===== INITIATE BATTLE =====//
/*
4/20/21 (lol weed)
Description: simply starts the battle, nothing crazy.

*/
public class InitiateBattle : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (IsBattleValid())
        {
            TurnSystem.StartBattle();
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    private bool IsBattleValid()
    {
        if (TurnSystem.enemyParty.Count == 0)
        {
            return false;
        }

        if (TurnSystem.everyoneLoaded)
        {
            return false;
        }

        return true;
    }
}
