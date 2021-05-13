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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameState(GameStates newState)
    {
        if (GameState == newState) { return; }
        GameState = newState;
    }
}
