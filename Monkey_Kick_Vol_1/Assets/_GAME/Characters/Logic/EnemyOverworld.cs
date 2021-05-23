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
    [SerializeField] private SceneReference battleScene;

    public override void Awake()
    {
        base.Awake();
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
            Game.SetGameState(GameStates.Battle);
            SceneManager.LoadScene(battleScene);
        }
    }
}
