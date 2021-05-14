//===== ENEMY CONTROLLER =====//
/*
5/14/21
Description:
- Controls all enemy logic.

*/

using UnityEngine;

[RequireComponent(typeof(EnemyOverworld))]
[RequireComponent(typeof(EnemyBattle))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    private EnemyOverworld enemyOverworld; // overworld logic
    private EnemyBattle enemyBattle; // battle logic

    #region COLLISION AND TRIGGERS

    [HideInInspector] public Rigidbody rb;

    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyOverworld = GetComponent<EnemyOverworld>();
        enemyBattle = GetComponent<EnemyBattle>();

        switch (Game.gameManager.GameState)
        {
            case GameStates.OVERWORLD:
            {
                enemyOverworld.StartOverworld();

                break;
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (Game.gameManager.GameState == GameStates.OVERWORLD)
        {
            enemyOverworld.OnCollisionEnterOverworld(col);
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (Game.gameManager.GameState == GameStates.OVERWORLD)
        {
            enemyOverworld.OnCollisionExitOverworld(col);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (Game.gameManager.GameState == GameStates.OVERWORLD)
        {
            enemyOverworld.TriggerBattle(col);
        }
    }
}
