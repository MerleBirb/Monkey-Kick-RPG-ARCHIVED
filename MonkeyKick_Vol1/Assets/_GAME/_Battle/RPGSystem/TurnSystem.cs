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

        [HideInInspector] public List<CharacterBattle> allCharacterList;
        [HideInInspector] public List<CharacterBattle> playerList;
        [HideInInspector] public List<CharacterBattle> enemyList;
        [HideInInspector] public List<TurnClass> TurnOrder;
        public List<TurnClass> GetTurnOrder() { return TurnOrder; }

        [HideInInspector] public CharacterBattle ActiveCharacter;
        [HideInInspector] public int TurnCounter = 0;

        // Start is called before the first frame update
        private void Start()
        {
            if (Game != null && Game.CompareGameState(GameStates.Battle))
            {
                ActiveCharacter = null;
                TurnCounter = 0;

                if (SetUpBattle.GetPlayerParty() != null) { SpawnPlayerParty(); }
                if (SetUpBattle.GetEnemyParty() != null) { SpawnEnemyParty(); }

                if (EveryoneLoaded())
                {
                    FillTurnOrder();
                    SetTurnOrder();
                    ActiveCharacter = TurnOrder[0].character;
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
            for (int p = 0; p < SetUpBattle.GetPlayerParty().CharacterList.Count; p++)
            {
                if (p == 0)
                {
                    // spawn player in
                    var playerLeader = Instantiate<CharacterBattle>(
                        playerPrefab,
                        SetUpBattle.GetPlayerParty().CharacterList[p].battlePos,
                        Quaternion.identity);

                    // set the stats and name
                    playerLeader.Stats = SetUpBattle.GetPlayerParty().CharacterList[p];
                    playerLeader.gameObject.name = playerLeader.Stats.CharacterName;

                    // set the animation information
                    playerLeader.GetComponent<CapsuleCollider>().height = playerLeader.Stats.Height;

                    allCharacterList.Add(playerLeader); // add to the all characters list
                    playerList.Add(playerLeader);
                }
                else
                {
                    // spawn player in
                    var playerPartyMember = Instantiate<CharacterBattle>(
                        playerPartyMemberPrefab,
                        SetUpBattle.GetPlayerParty().CharacterList[p].battlePos,
                        Quaternion.identity);

                    // set the stats and name
                    playerPartyMember.Stats = SetUpBattle.GetPlayerParty().CharacterList[p];
                    playerPartyMember.gameObject.name = playerPartyMember.Stats.CharacterName;

                    // set the animation information
                    playerPartyMember.GetComponent<CapsuleCollider>().height = playerPartyMember.Stats.Height;

                    allCharacterList.Add(playerPartyMember); // add to the all characters list
                    playerList.Add(playerPartyMember);
                }
            }
        }

        private void SpawnEnemyParty()
        {
            for (int e = 0; e < SetUpBattle.GetEnemyParty().CharacterList.Count; e++)
            {
                if (e == 0)
                {
                    // spawn enemy in
                    var enemyLeader = Instantiate<CharacterBattle>(
                        enemyPrefab,
                        SetUpBattle.GetEnemyParty().CharacterList[e].battlePos,
                        Quaternion.identity);

                    // set the stats and the name
                    enemyLeader.Stats = SetUpBattle.GetEnemyParty().CharacterList[e];
                    enemyLeader.gameObject.name = enemyLeader.Stats.CharacterName;

                    // set the animation information
                    enemyLeader.GetComponent<CapsuleCollider>().height = enemyLeader.Stats.Height;

                    allCharacterList.Add(enemyLeader); // add to the all characters list
                    enemyList.Add(enemyLeader);
                }
                else
                {
                    // spawn enemy in
                    var enemyPartyMember = Instantiate<CharacterBattle>(
                        enemyPartyMemberPrefab,
                        SetUpBattle.GetEnemyParty().CharacterList[e].battlePos,
                        Quaternion.identity);

                    // set the stats and the name
                    enemyPartyMember.Stats = SetUpBattle.GetEnemyParty().CharacterList[e];
                    enemyPartyMember.gameObject.name = enemyPartyMember.Stats.CharacterName;

                    // set the animation information
                    enemyPartyMember.GetComponent<CapsuleCollider>().height = enemyPartyMember.Stats.Height;

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
                TurnOrder.Add(allCharacterList[i].Turn);
                TurnOrder[i].character = allCharacterList[i];
            }

            for (int j = 0; j < TurnOrder.Count; j++)
            {
                TurnOrder[j].turnSystem = this;
                TurnOrder[j].charSpeed = TurnOrder[j].character.Stats.Speed.Stat;
                TurnOrder[j].charName = TurnOrder[j].character.Stats.CharacterName;
            }
        }

        private void SetTurnOrder()
        {
            TurnOrder.Sort((a, b) =>
            {
                var speedA = a.charSpeed.Value;
                var speedB = b.charSpeed.Value;

            // sort the speeds
            return speedA < speedB ? 1 : (speedA == speedB ? 0 : -1);
            });
        }

        private void UpdateTurns() // cycles through the turn order 
        {
            for (int i = 0; i < TurnOrder.Count; i++)
            {
                if (!TurnOrder[i].wasTurnPrev)
                {
                    TurnOrder[i].isTurn = true;
                    break;
                }
                else if ((i == TurnOrder.Count - 1) && (TurnOrder[i].wasTurnPrev))
                {
                    SetTurnOrder();
                    ResetTurns();
                    TurnCounter++;
                }

                if (ActiveCharacter.transform.position == ActiveCharacter.Stats.battlePos)
                {
                    if (TurnOrder[i].isTurn) { ActiveCharacter = TurnOrder[i].character; }
                }

                if (enemyList.Count == 0)
                {
                    EndBattle();
                }
            }
        }

        private void ResetTurns() // reset the turn order after every character has gone.
        {
            for (int i = 0; i < TurnOrder.Count; i++)
            {
                if (i == 0)
                {
                    TurnOrder[i].isTurn = true;
                    TurnOrder[i].wasTurnPrev = false;
                }
                else
                {
                    TurnOrder[i].isTurn = false;
                    TurnOrder[i].wasTurnPrev = false;
                }

                if (TurnOrder[i] == null)
                {
                    TurnOrder.Remove(TurnOrder[i]);
                }
            }
        }

        public void RemovePlayerFromParty(CharacterBattle player)
        {
            playerList.Remove(player);
        }

        public void RemoveEnemyFromParty(CharacterBattle enemy)
        {
            enemyList.Remove(enemy);
        }

        private void EndBattle()
        {
            Game.SetGameState(GameStates.Overworld);
            SetUpBattle.LoadPreviousScene();
        }

    }
}