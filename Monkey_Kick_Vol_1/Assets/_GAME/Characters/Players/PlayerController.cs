//===== PLAYER CONTROLLER =====//
/*
5/11/21
Description: 
- Controls all the player logic. 
- Requires PlayerMovement and PlayerBattle components.

*/

using UnityEngine;


[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerBattle))]
public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement; // movement logic
    private PlayerBattle playerBattle; // battle logic

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerBattle = GetComponent<PlayerBattle>();

        switch(Game.gameManager.GameState)
        {
            case GameStates.OVERWORLD:
            {
                playerMovement.StartMovement();

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
                playerMovement.UpdateMovement();

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
                playerMovement.FixedUpdateMovement();

                break;
            }
        }
    }
}