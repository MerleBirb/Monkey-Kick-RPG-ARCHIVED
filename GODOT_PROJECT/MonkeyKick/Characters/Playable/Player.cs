using Godot;
using System;
using Merlebirb.PlayableCharacter;
using Merlebirb.Managers;

//===== PLAYER =====//
/*
4/1/21 (april fools trolled lol)
Description: this script decides which scripts inside the player heirarchy depending on the game state.

*/

public class Player : KinematicBody
{
    // store the player nodes
    private PlayerMovement playerMovement;
    private string overworldNode = "Overworld";

    // Called when the node enters the scene tree for the first time.
        public override void _Ready()
    {
        SetMeta("Type", "Player");
        playerMovement = GetNode<PlayerMovement>(overworldNode);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        switch(GameManager.state)
        {
            case GameStates.OVERWORLD:
            {
                playerMovement.PlayerMovementProcess();
                break;
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        switch(GameManager.state)
        {
            case GameStates.OVERWORLD:
            {
                playerMovement.PlayerMovementPhysics();
                break;
            }
        }
    }
}
