//===== GAME MANAGER =====//
/*
5/12/21
Description:
- Handles important game data and the game state

*/

using UnityEngine;

public enum GameStates
{
    OVERWORLD,
    BATTLE,
    MENU,
    CUTSCENE,
    PAUSE
}

public class GameManager : MonoBehaviour
{
    public GameStates GameState;
    
    public void ChangeGameState(GameStates newState)
    {
        if (GameState == newState) { return; }
        GameState = newState;
    }
}
