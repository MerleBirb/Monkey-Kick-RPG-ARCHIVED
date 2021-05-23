//===== GAME STATE DATA =====//
/*
5/23/21
Description:
- The game state.
- Handles what logic is run and what isnt at the time.

Author: Merlebirb
*/

using UnityEngine;
public enum GameStates
{
    Overworld = 100,
    Battle = 200,
    Cutscene = 300,
    Menu = 400,
    Pause = 500
}

[CreateAssetMenu(menuName = "New Data/Game State Data", fileName = "GameState")]
public class GameStateData : ScriptableObject
{
    [SerializeField] private GameStates GameState;

    public GameStates GetGameState() { return GameState; }
    public void SetGameState(GameStates newState) { GameState = newState; }

    public bool CompareGameState(GameStates comparisonState) 
    { 
        bool isTheSame = false;
        
        if (GameState == comparisonState)
        {
            isTheSame = true;
        }

        return isTheSame;  
    }
}
