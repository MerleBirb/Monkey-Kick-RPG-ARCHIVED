//===== TURN SYSTEM =====//
/*
5/23/21
Description:
- Turn system for battles.

Author: Merlebirb
*/

using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] private GameStateData Game;

    #region CHARACTER PREFABS

    public CharacterBattle playerPrefab;
    public CharacterBattle playerPartyMemberPrefab;
    public CharacterBattle enemyPrefab;
    public CharacterBattle enemyPartyMemberPrefab;

    #endregion

    [ReadOnly] [SerializeField] private List<CharacterBattle> allCharacterList;

    // Start is called before the first frame update
    private void Start()
    {
        if (Game != null && Game.CompareGameState(GameStates.Battle))
        {
            if (BattleParties.GetPlayerParty() != null) { SpawnPlayerParty(); }
            if (BattleParties.GetEnemyParty() != null) { SpawnEnemyParty(); }
        }
    }

    private void SpawnPlayerParty()
    {
        for (int p = 0; p < BattleParties.GetPlayerParty().party.Count; p++)
        {
            if (p == 0)
            {
                // spawn player in
                var playerLeader = Instantiate<CharacterBattle>(
                    playerPrefab, 
                    BattleParties.GetPlayerParty().party[p].battlePos, 
                    Quaternion.identity);

                // set the stats and name
                playerLeader.stats = BattleParties.GetPlayerParty().party[p];
                playerLeader.gameObject.name = playerLeader.stats.CharacterName;

                allCharacterList.Add(playerLeader); // add to the all characters list
            }
            else
            {
                // spawn player in
                var playerPartyMember = Instantiate<CharacterBattle>(
                    playerPartyMemberPrefab, 
                    BattleParties.GetPlayerParty().party[p].battlePos, 
                    Quaternion.identity);

                // set the stats and name
                playerPartyMember.stats = BattleParties.GetPlayerParty().party[p];
                playerPartyMember.gameObject.name = playerPartyMember.stats.CharacterName;

                allCharacterList.Add(playerPartyMember); // add to the all characters list
            }
        }
    }

    private void SpawnEnemyParty()
    {
        for (int e = 0; e < BattleParties.GetEnemyParty().party.Count; e++)
        {
            if (e == 0)
            {
                // spawn enemy in
                var enemyLeader = Instantiate<CharacterBattle>(
                    enemyPrefab, 
                    BattleParties.GetEnemyParty().party[e].battlePos, 
                    Quaternion.identity);
                
                // set the stats and the name
                enemyLeader.stats = BattleParties.GetEnemyParty().party[e];
                enemyLeader.gameObject.name = enemyLeader.stats.CharacterName;

                allCharacterList.Add(enemyLeader); // add to the all characters list
            }
            else
            {
                // spawn enemy in
                var enemyPartyMember = Instantiate<CharacterBattle>(
                    enemyPartyMemberPrefab, 
                    BattleParties.GetEnemyParty().party[e].battlePos, 
                    Quaternion.identity);

                // set the stats and the name
                enemyPartyMember.stats = BattleParties.GetEnemyParty().party[e];
                enemyPartyMember.gameObject.name = enemyPartyMember.stats.CharacterName;

                allCharacterList.Add(enemyPartyMember); // add to the all characters list
            }
        }
    }
}
