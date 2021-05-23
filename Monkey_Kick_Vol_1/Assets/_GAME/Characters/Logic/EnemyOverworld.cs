//===== ENEMY OVERWORLD =====//
/*
5/23/21
Description:
- Contains enemy movement logic for the overworld game state.

Author: Merlebirb
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyOverworld : CharacterOverworld
{
    private CharacterParty _enemyParty;
    [SerializeField] private SceneReference battleScene;

    public override void Awake()
    {
        base.Awake();

        _enemyParty = GetComponent<CharacterParty>();
    }

    public override void Update()
    {
        base.Update();
    }
    
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            // save the parties into the battle parties data
            var _playerParty = col.GetComponent<CharacterParty>().characterParty;

            BattleParties.SetPlayerParty(_playerParty);
            BattleParties.SetEnemyParty(_enemyParty.characterParty);

            Game.SetGameState(GameStates.Battle);
            SceneManager.LoadScene(battleScene);
        }
    }
}
