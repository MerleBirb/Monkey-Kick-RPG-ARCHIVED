// Merle Roji 7/12/22

using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.Characters;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.Managers.TurnSystem
{
    /// <summary>
    /// Manages the logic of the turn based system.
    /// 
    /// Notes:
    /// 
    /// </summary>
    public class TurnManager : MonoBehaviour
    {
        // lists of two incoming parties
        [SerializeField] private PartyManager _incomingPlayerParty;
        [SerializeField] private PartyManager _incomingEnemyParty;

        // lists of current parties
        private List<CharacterBattle> _playerParty = new List<CharacterBattle>();
        private List<CharacterBattle> _enemyParty = new List<CharacterBattle>();
        private List<CharacterBattle> _allParty = new List<CharacterBattle>();
        private List<Turn> _turnOrder = new List<Turn>();
        public List<Turn> TurnOrder { get => _turnOrder; }

        // lists of spawns for enemies and players
        [SerializeField] private Transform[] _playerSpawns;
        [SerializeField] private Transform[] _enemySpawns;

        // checks
        private bool _areAllCharactersSpawned;
        private bool _areAllCharactersInTheTurnOrder;

        private void Awake()
        {
            FillTurnOrder();
            SpawnCharacters();
            SortTurnOrder();
            ResetTurns();
        }

        private void Update()
        {
            UpdateTurns();
        }

        private void OnDisable()
        {
            ResetBattle();
        }

        private void FillTurnOrder()
        {
            _allParty.AddRange(_incomingPlayerParty.Characters);
            _allParty.AddRange(_incomingEnemyParty.Characters);

            // set the new lists
            for (int i = 0; i < _allParty.Count; ++i)
            {
                _turnOrder.Add(_allParty[i].Turn);
                _turnOrder[i].Character = _allParty[i].GetComponent<Character>();
                _turnOrder[i].Speed = _turnOrder[i].Character.Stats.Speed;

                if (_allParty[i].CompareTag(TagsQoL.PLAYER_TAG))
                {
                    _playerParty.Add(_allParty[i]);
                }

                if (_allParty[i].CompareTag(TagsQoL.ENEMY_TAG))
                {
                    _enemyParty.Add(_allParty[i]);
                }
            }
        }

        private void SpawnCharacters()
        {
            for (int p = 0; p < _playerParty.Count; ++p)
            {
                CharacterBattle newPlayer = Instantiate(_playerParty[p], _playerSpawns[p].position, Quaternion.identity);
                _playerParty[p] = newPlayer;
            }

            for (int e = 0; e < _enemyParty.Count; ++e)
            {
                CharacterBattle newEnemy = Instantiate(_enemyParty[e], _enemySpawns[e].position, Quaternion.identity);
                _enemyParty[e] = newEnemy;
            }
        }

        private void SortTurnOrder()
        {
            _turnOrder.Sort((a, b) =>
            {
                var speedA = a.Speed;
                var speedB = b.Speed;

                // sort the speeds
                return speedA < speedB ? 1 : (speedA == speedB ? 0 : -1);
            });
        }

        private void ResetTurns()
        {
            for (int i = 0; i < _turnOrder.Count; ++i)
            {
                if (i == 0)
                {
                    _turnOrder[i].IsTurn = true;
                    _turnOrder[i].WasTurnPrev = false;
                }
                else
                {
                    _turnOrder[i].IsTurn = false;
                    _turnOrder[i].WasTurnPrev = false;
                }
            }
        }

        private void UpdateTurns()
        {
            for (int i = 0; i < _turnOrder.Count; ++i)
            {
                if (!_turnOrder[i].WasTurnPrev)
                {
                    if (CheckIfCharacterIsDead(i)) continue;
                    _turnOrder[i].IsTurn = true;
                    break;
                }
                else if (i == _turnOrder.Count - 1 && _turnOrder[i].WasTurnPrev)
                {
                    if (EnemyPartyDefeated())
                    {
                        return;
                    }

                    ResetTurns();
                }
            }
        }

        // remove the character if they're dead
        private bool CheckIfCharacterIsDead(int index)
        {
            if (_turnOrder[index].IsDead)
            {
                if (_turnOrder[index].Character.CompareTag(TagsQoL.PLAYER_TAG))
                {
                    _playerParty.Remove(_turnOrder[index].Character.GetComponent<CharacterBattle>());
                }
                else if (_turnOrder[index].Character.CompareTag(TagsQoL.ENEMY_TAG))
                {
                    _enemyParty.Remove(_turnOrder[index].Character.GetComponent<CharacterBattle>());
                }
                _turnOrder.RemoveAt(index);

                return true;
            }

            return false;
        }

        // check if the enemy party has been wiped out
        public bool EnemyPartyDefeated()
        {
            if (_enemyParty.Count == 0) return true;
            return false;
        }

        // end battle and reset every list
        private void ResetBattle()
        {
            _allParty.Clear();
            _playerParty.Clear();
            _enemyParty.Clear();
            _turnOrder.Clear();
            _areAllCharactersInTheTurnOrder = false;
            _areAllCharactersSpawned = false;
        }
    }
}