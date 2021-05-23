//===== BATTLE PARTIES =====//
/*
5/23/21
Description:
- Holds the parties going into battle between scenes.

Author: Merlebirb
*/

using UnityEngine;
public static class BattleParties
{
    private static CharacterPartyData playerParty;
    private static CharacterPartyData enemyParty;

    public static CharacterPartyData GetPlayerParty() { return playerParty; }
    public static CharacterPartyData GetEnemyParty() { return enemyParty; }

    public static void SetPlayerParty(CharacterPartyData newParty)
    {
        if (playerParty == newParty) return;

        playerParty = null;
        playerParty = newParty;

        Debug.Log(">>> PLAYER PARTY COUNT: " + playerParty.party.Count);
    }

    public static void SetEnemyParty(CharacterPartyData newParty)
    {
        if (enemyParty == newParty) return;

        enemyParty = null;
        enemyParty = newParty;
        
        Debug.Log(">>> ENEMY PARTY COUNT: " + enemyParty.party.Count);
    }

    public static void ClearPlayerParty() { playerParty = null; }
    public static void ClearEnemyParty() { enemyParty = null; }
}
