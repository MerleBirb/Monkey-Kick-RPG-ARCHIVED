//===== TURN SYSTEM =====//
/*
5/19/21
Description:
- Handles the turn based combat loop.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public List<Transform> playerSpawns;
    public List<Transform> enemySpawns;
    
    // DELETE LATER
    private void Awake()
    {
        if (Game.gameManager.GameState == GameStates.BATTLE)
        {
            var player = Instantiate(Game.partyManager.PlayerParty[0], playerSpawns[0].position, playerSpawns[0].rotation);
            Game.partyManager.TemporarilyLoadPlayerStats(player.GetComponent<PlayableController>());
        }
    }
}
