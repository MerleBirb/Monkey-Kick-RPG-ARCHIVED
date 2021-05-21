//===== PLAYER CONTROLLER =====//
/*
5/11/21
Description: 
- Controls all player logic.

*/

using UnityEngine;

[RequireComponent(typeof(PlayerOverworld))]
[RequireComponent(typeof(PlayerBattle))]
public class PlayerController : PlayableController
{
    #region OVERWORLD 

    private PlayerOverworld playerOverworld; // overworld logic

    #endregion

    #region BATTLE

    private PlayerBattle playerBattle; // battle logic

    #endregion

    public override void Start()
    {   
        base.Start();

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