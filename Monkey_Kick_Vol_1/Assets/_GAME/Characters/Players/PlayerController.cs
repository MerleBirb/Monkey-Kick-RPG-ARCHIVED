//===== PLAYER CONTROLLER =====//
/*
5/11/21
Description: 
- Controls all player logic.

*/

using UnityEngine;


[RequireComponent(typeof(PlayerOverworld))]
[RequireComponent(typeof(PlayerBattle))]
public class PlayerController : MonoBehaviour
{
    private PlayerOverworld playerOverworld; // overworld logic
    private PlayerBattle playerBattle; // battle logic

    public PlayerInfo stats;

    private void Start()
    {
        playerOverworld = GetComponent<PlayerOverworld>();
        playerBattle = GetComponent<PlayerBattle>();

        switch(Game.gameManager.GameState)
        {
            case GameStates.OVERWORLD:
            {
                playerOverworld.StartOverworld();

                break;
            }
        }
    }

    private void Update()
    {
        switch(Game.gameManager.GameState)
        {
            case GameStates.OVERWORLD:
            {
                playerOverworld.UpdateOverworld();

                break;
            }
        }
    }

    private void FixedUpdate()
    {
        switch(Game.gameManager.GameState)
        {
            case GameStates.OVERWORLD:
            {
                playerOverworld.FixedUpdateOverworld();

                break;
            }
        }
    }
}