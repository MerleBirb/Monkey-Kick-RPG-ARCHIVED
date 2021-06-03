//===== TURN SYSTEM =====//
/*
5/23/21
Description:
- Turn system for battles.

Author: Merlebirb
*/

using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;
using MonkeyKick.Managers;
using MonkeyKick.Overworld;

namespace MonkeyKick.Battle
{
    public class TurnSystem : MonoBehaviour
    {
        [SerializeField] private GameStateData Game;

        #region CHARACTER PREFABS

        public CharacterBattle playerPrefab;
        public CharacterBattle playerPartyMemberPrefab;
        public CharacterBattle enemyPrefab;
        public CharacterBattle enemyPartyMemberPrefab;

        #endregion

        [ReadOnly] public List<CharacterBattle> allCharacterList;
        [ReadOnly] public List<CharacterBattle> playerList;
        [ReadOnly] public List<CharacterBattle> enemyList;
        [ReadOnly] public List<TurnClass> turnOrder;
        public List<TurnClass> GetTurnOrder() { return turnOrder; }

        [ReadOnly] public CharacterBattle ActiveCharacter;
        [ReadOnly] public int TurnCounter = 0;

        // Start is called before the first frame update
        private void Start()
        {
            if (Game != null && Game.CompareGameState(GameStates.Battle))
            {
                ActiveCharacter = null;
                TurnCounter = 0;

                if (BattleParties.GetPlayerParty() != null) { SpawnPlayerParty(); }
                if (BattleParties.GetEnemyParty() != null) { SpawnEnemyParty(); }

                if (EveryoneLoaded())
                {
                    Debug.Log("All characters have been loaded into the scene and into the turn order. "
                    + "Setting the turn order...");

                    FillTurnOrder();
                    SetTurnOrder();
                    ActiveCharacter = turnOrder[0].character;
                    ResetTurns();
                }
            }
        }

        private void Update()
        {
            UpdateTurns();
        }

        private void SpawnPlayerParty()
        {
            for (int p = 0; p < BattleParties.GetPlayerParty().CharacterList.Count; p++)
            {
                if (p == 0)
                {
                    // spawn player in
                    var playerLeader = Instantiate<CharacterBattle>(
                        playerPrefab,
                        BattleParties.GetPlayerParty().CharacterList[p].battlePos,
                        Quaternion.identity);

                    // set the stats and name
                    playerLeader.Stats = BattleParties.GetPlayerParty().CharacterList[p];
                    playerLeader.gameObject.name = playerLeader.Stats.CharacterName;

                    allCharacterList.Add(playerLeader); // add to the all characters list
                    playerList.Add(playerLeader);
                }
                else
                {
                    // spawn player in
                    var playerPartyMember = Instantiate<CharacterBattle>(
                        playerPartyMemberPrefab,
                        BattleParties.GetPlayerParty().CharacterList[p].battlePos,
                        Quaternion.identity);

                    // set the stats and name
                    playerPartyMember.Stats = BattleParties.GetPlayerParty().CharacterList[p];
                    playerPartyMember.gameObject.name = playerPartyMember.Stats.CharacterName;

                    allCharacterList.Add(playerPartyMember); // add to the all characters list
                    playerList.Add(playerPartyMember);
                }
            }
        }

        private void SpawnEnemyParty()
        {
            for (int e = 0; e < BattleParties.GetEnemyParty().CharacterList.Count; e++)
            {
                if (e == 0)
                {
                    // spawn enemy in
                    var enemyLeader = Instantiate<CharacterBattle>(
                        enemyPrefab,
                        BattleParties.GetEnemyParty().CharacterList[e].battlePos,
                        Quaternion.identity);

                    // set the stats and the name
                    enemyLeader.Stats = BattleParties.GetEnemyParty().CharacterList[e];
                    enemyLeader.gameObject.name = enemyLeader.Stats.CharacterName;

                    allCharacterList.Add(enemyLeader); // add to the all characters list
                    enemyList.Add(enemyLeader);
                }
                else
                {
                    // spawn enemy in
                    var enemyPartyMember = Instantiate<CharacterBattle>(
                        enemyPartyMemberPrefab,
                        BattleParties.GetEnemyParty().CharacterList[e].battlePos,
                        Quaternion.identity);

                    // set the stats and the name
                    enemyPartyMember.Stats = BattleParties.GetEnemyParty().CharacterList[e];
                    enemyPartyMember.gameObject.name = enemyPartyMember.Stats.CharacterName;

                    allCharacterList.Add(enemyPartyMember); // add to the all characters list
                    enemyList.Add(enemyPartyMember);
                }
            }
        }

        private bool EveryoneLoaded()
        {
            return (playerList.Count + enemyList.Count == allCharacterList.Count);
        }

        private void FillTurnOrder() // fills up the turn order list
        {
            for (int i = 0; i < allCharacterList.Count; i++)
            {
                turnOrder.Add(allCharacterList[i].Turn);
                turnOrder[i].character = allCharacterList[i];
            }

            for (int j = 0; j < turnOrder.Count; j++)
            {
                turnOrder[j].turnSystem = this;
                turnOrder[j].charSpeed = turnOrder[j].character.Stats.Speed.Stat;
                turnOrder[j].charName = turnOrder[j].character.Stats.CharacterName;
            }
        }

        private void SetTurnOrder()
        {
            turnOrder.Sort((a, b) =>
            {
                var speedA = a.charSpeed.Value;
                var speedB = b.charSpeed.Value;

            // sort the speeds
            return speedA < speedB ? 1 : (speedA == speedB ? 0 : -1);
            });
        }

        private void UpdateTurns() // cycles through the turn order 
        {
            for (int i = 0; i < turnOrder.Count; i++)
            {
                if (!turnOrder[i].wasTurnPrev)
                {
                    turnOrder[i].isTurn = true;
                    break;
                }
                else if ((i == turnOrder.Count - 1) && (turnOrder[i].wasTurnPrev))
                {
                    SetTurnOrder();
                    ResetTurns();
                    TurnCounter++;
                }

                if (ActiveCharacter.transform.position == ActiveCharacter.Stats.battlePos)
                {
                    if (turnOrder[i].isTurn) { ActiveCharacter = turnOrder[i].character; }
                }
            }
        }

        private void ResetTurns() // reset the turn order after every character has gone.
        {
            for (int i = 0; i < turnOrder.Count; i++)
            {
                if (i == 0)
                {
                    turnOrder[i].isTurn = true;
                    turnOrder[i].wasTurnPrev = false;
                }
                else
                {
                    turnOrder[i].isTurn = false;
                    turnOrder[i].wasTurnPrev = false;
                }

                if (turnOrder[i] == null)
                {
                    turnOrder.Remove(turnOrder[i]);
                }
            }
        }

    }
}