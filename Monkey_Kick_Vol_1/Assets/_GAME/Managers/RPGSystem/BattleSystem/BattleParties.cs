//===== BATTLE PARTIES =====//
/*
5/23/21
Description:
- Holds the parties going into battle between scenes.

Author: Merlebirb
*/

using UnityEngine;

[CreateAssetMenu(menuName = "New Battle Parties Data", fileName = "BattleParties")]
public class BattleParties : ScriptableObject
{
    [ReadOnly] public CharacterPartyData playerParty;
    [ReadOnly] public CharacterPartyData enemyParty;

    public void SetPlayerParty(CharacterPartyData newParty)
    {
        if (playerParty == newParty) return;

        playerParty = null;
        playerParty = newParty;
    }

    public void SetEnemyParty(CharacterPartyData newParty)
    {
        if (enemyParty == newParty) return;

        enemyParty = null;
        enemyParty = newParty;
    }
}
