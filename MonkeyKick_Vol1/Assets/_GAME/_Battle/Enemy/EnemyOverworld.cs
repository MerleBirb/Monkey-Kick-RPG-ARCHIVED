//===== ENEMY OVERWORLD =====//
/*
5/23/21
Description:
- Contains enemy movement logic for the overworld game state.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using MonkeyKick.Managers;
using MonkeyKick.Overworld;

namespace MonkeyKick.Battle
{
    public class EnemyOverworld : CharacterOverworld
    {
        private CharacterParty _enemyParty;
        [SerializeField] private string battleScene;

        public override void Awake()
        {
            base.Awake();

            _enemyParty = GetComponent<CharacterParty>();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (Game.CompareGameState(GameStates.Overworld))
            {
                if (col.CompareTag("Player"))
                {
                    // save the parties into the battle parties data
                    var _playerParty = col.GetComponent<CharacterParty>().Party;

                    BattleParties.SetPlayerParty(_playerParty);
                    BattleParties.SetEnemyParty(_enemyParty.Party);

                    Game.SetGameState(GameStates.Battle);
                    SceneManager.LoadScene(battleScene);
                }
            }
        }
    }
}