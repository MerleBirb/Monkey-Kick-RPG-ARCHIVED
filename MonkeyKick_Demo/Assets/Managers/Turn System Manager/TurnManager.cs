// Merle Roji 7/12/22

using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.Characters;

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
        [SerializeField] private PartyManager _incomingEnemyParty;
        [SerializeField] private PartyManager _incomingPlayerParty;

        // lists of two current parties
        private List<CharacterBattle> _playerParty;
        private List<CharacterBattle> _enemyParty;

        // lists of spawns for enemies and players
        [SerializeField] private Transform[] _playerSpawns;
        [SerializeField] private Transform[] _enemySpawns;

        private void Awake()
        {
            SpawnCharacters();
        }

        private void SpawnCharacters()
        {
            _playerParty = new List<CharacterBattle>();
            _enemyParty = new List<CharacterBattle>();

            // spawn players
            for (int p = 0; p < _playerSpawns.Length; p++)
            {
                CharacterBattle newPlayer = Instantiate(_incomingPlayerParty.Characters[p], _playerSpawns[p].position, Quaternion.identity);
                _playerParty.Add(newPlayer);
            }

            // spawn enemies
            for (int e = 0; e < _enemySpawns.Length; e++)
            {
                CharacterBattle newEnemy = Instantiate(_incomingEnemyParty.Characters[e], _enemySpawns[e].position, Quaternion.identity);
                _enemyParty.Add(newEnemy);
            }
        }
    }
}

