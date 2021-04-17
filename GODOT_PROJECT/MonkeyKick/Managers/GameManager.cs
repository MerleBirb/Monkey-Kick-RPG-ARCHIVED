using Godot;
using System;
using System.Collections.Generic;

namespace Merlebirb.Managers
{
    //===== GAME MANAGER =====//
    /*
    4/1/21 (april fools lol trolled)
    Description: An extremely important script that controls aspects of the game's state and holds very important variables.

    */
    public enum GameStates // various states the game can be in, might add more depending on what is in the game.
    {
        MAIN_MENU = 100,
        OVERWORLD = 200,
        BATTLE = 300,
        CUTSCENE = 400,
        PAUSED = 500
    }

    public class GameManager : Node
    {
        public static GameStates state; // controls what the game manager is doing depending on the game state 
        public static List<Node> playerParty = new List<Node>();

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            if (GetTree().CurrentScene.Name.Contains("Title"))
            {
                state = GameStates.MAIN_MENU;
            }
            else if (GetTree().CurrentScene.Name.Contains("Overworld"))
            {
                state = GameStates.OVERWORLD;
            }
            else if (GetTree().CurrentScene.Name.Contains("Battle"))
            {
                state = GameStates.BATTLE;
            }
            else
            {
                state = GameStates.OVERWORLD;
            }

            AddPartyMember("res://Characters/Playable/P-Dawg/P-Dawg.tscn");
            GD.Print("Size of player party: " + playerParty.Count);
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(float delta)
        {
            switch(state)
            {
                case GameStates.MAIN_MENU:
                {
                    break;
                }
                case GameStates.OVERWORLD:
                {
                    break;
                }
                case GameStates.BATTLE:
                {
                    break;
                }
                case GameStates.CUTSCENE:
                {
                    break;
                }
                case GameStates.PAUSED:
                {
                    break;
                }
            }
        }

        public static void ChangeGameState(GameStates newState)
        {
            if (state == newState) { return; };
            state = newState;
        }

        public static void AddPartyMember(string resourcePath)
        {
            PackedScene newPartyMember = GD.Load<PackedScene>(resourcePath);
            Node instance = newPartyMember.Instance();

            if (playerParty.Count == 0)
            {
                playerParty.Add(instance);
                GD.Print("Added first party member.");
            }
            else
            {
                if (playerParty[playerParty.Count - 1] == instance)
                {
                    GD.PrintErr("Error: party member already exists.");
                    return;                    
                }
                else
                {
                    playerParty.Add(instance);
                    GD.Print("Added next party member.");
                }
            }

            GD.Print("Current Player Party: ");
            for (int i = 0; i < playerParty.Count; i++)
            {                
                GD.Print(playerParty[i].Name);
            }
        }
    }

}
