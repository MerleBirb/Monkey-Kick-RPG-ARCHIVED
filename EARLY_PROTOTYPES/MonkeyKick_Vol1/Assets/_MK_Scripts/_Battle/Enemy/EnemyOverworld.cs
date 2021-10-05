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
        private EnemyBattle _battle;
        [SerializeField] private string battleScene;

        public override void Awake()
        {
            base.Awake();

            _battle = GetComponent<EnemyBattle>();

            _enemyParty = GetComponent<CharacterParty>();
        }

        private void Start()
        {
            if (!_battle.Stats.isAlive) gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider col)
        {
            if (Game.CompareGameState(GameStates.Overworld))
            {
                if (col.CompareTag("Player"))
                {
                    // save the parties into the battle parties data
                    var _playerParty = col.GetComponent<CharacterParty>().Party;

                    SetUpBattle.SetPlayerParty(_playerParty);
                    SetUpBattle.SetEnemyParty(_enemyParty.Party);
                    SetUpBattle.SavePreviousScene();

                    Game.SetGameState(GameStates.Battle);
                    SceneManager.LoadScene(battleScene);
                }
            }
        }
    }
}