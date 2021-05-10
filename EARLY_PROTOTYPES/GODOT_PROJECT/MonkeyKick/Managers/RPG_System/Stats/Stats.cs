using Godot;
using System;
using Merlebirb.TurnBasedSystem;

//===== STATS =====//
/*
4/2/21
Description: Holds the character's stats and manages them if need be.

*/

public class Stats : Node
{
    [Export] public Character charStats = new Character();

    //public override void _Ready()
    //{
    //   
    //}

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
