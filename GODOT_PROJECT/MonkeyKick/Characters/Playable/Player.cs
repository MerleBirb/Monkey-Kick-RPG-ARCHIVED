using Godot;
using System;
using Merlebirb.PlayableCharacter;
using Merlebirb.Managers;
using Merlebirb.QualityOfLife;
using Merlebirb.Tag;

//===== PLAYER =====//
/*
4/1/21 (april fools trolled lol)
Description: this script decides which scripts inside the player heirarchy depending on the game state.

*/

public class Player : KinematicBody
{
    // store the player nodes
    private PlayerMovement playerMovement;
    private PlayerBattle playerBattle;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerMovement = GetNode<PlayerMovement>("Overworld");
        playerBattle = GetNode<PlayerBattle>("Battle");

        if (!this.HasTag("player"))
        {
            this.AddTag("player");
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        switch(GameManager.state)
        {
            case GameStates.OVERWORLD:
            {
                if (!playerMovement.Visible) { playerMovement.Visible = true; };
                if (playerBattle.Visible) { playerBattle.Visible = false; };

                playerMovement.PlayerMovementProcess();
                break;
            }
            case GameStates.BATTLE:
            {
                if (playerMovement.Visible) { playerMovement.Visible = false; };
                if (!playerBattle.Visible) { playerBattle.Visible = true; };

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
