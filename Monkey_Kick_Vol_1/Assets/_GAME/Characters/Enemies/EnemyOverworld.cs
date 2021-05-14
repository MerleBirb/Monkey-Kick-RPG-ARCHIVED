//===== ENEMY OVERWORLD =====//
/*
5/14/21
Description:
- Contains enemy physics and movement logic for the overworld game state.

*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyOverworld : MonoBehaviour
{
    public string battleScene;

    #region COMPONENTS

    private Rigidbody rb;

    #endregion

    public void StartOverworld()
    {
        rb = GetComponent<EnemyController>().rb;
    }

    public void OnCollisionEnterOverworld(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // freeze when touching a player
            rb.isKinematic = true;
        }
    }

    public void OnCollisionExitOverworld(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // freeze when touching a player
            rb.isKinematic = false;
        }
    }

    public void TriggerBattle(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            InitiateBattle();
        }
    }
    
    public void InitiateBattle()
    {
        Game.gameManager.ChangeGameState(GameStates.BATTLE);
        SceneManager.LoadScene(battleScene);
    }
}
