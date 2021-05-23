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
    [SerializeField] private BattleParties battleParties;
    private CharacterParty _characterParty;
    [SerializeField] private SceneReference battleScene;

    public override void Awake()
    {
        base.Awake();

        _characterParty = GetComponent<CharacterParty>();
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
            var playerParty = col.GetComponent<CharacterParty>().characterParty;
            battleParties.SetPlayerParty(playerParty);
            battleParties.SetEnemyParty(_characterParty.characterParty);

            Game.SetGameState(GameStates.Battle);
            SceneManager.LoadScene(battleScene);
        }
    }
}
