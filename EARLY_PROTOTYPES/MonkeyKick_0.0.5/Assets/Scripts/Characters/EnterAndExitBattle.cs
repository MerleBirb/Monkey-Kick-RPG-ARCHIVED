using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterAndExitBattle : MonoBehaviour
{
    ////////// ENTERING AND EXITING BATTLE //////////

    // store the game manager
    public GameManager gameManager;
    public TurnSystemScript turnSystem;

    // store the rigidbody
    private Rigidbody rb;

    // store the scripts for battle and movement for players
    private PlayerMovement playerMovement;
    private PlayerBattleScript playerBattle;

    // store spawn points
    public List<GameObject> playerSpawns;
    public List<GameObject> enemySpawns;

    // store the respawn points
    private Vector3 returnPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        playerMovement = GetComponent<PlayerMovement>();
        playerBattle = GetComponent<PlayerBattleScript>();
        
        playerBattle.currentHP = playerBattle.charStats.maxHP;
        playerBattle.currentEnergy = playerBattle.charStats.maxEnergy;

        playerSpawns.AddRange(GameObject.FindGameObjectsWithTag("PlayerSpawner"));
        enemySpawns.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawner"));
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!GameManager.inBattle)
        {
            if (!playerMovement.enabled)
            {
                playerMovement.enabled = true;
            }

            if (playerBattle.enabled)
            {
                playerBattle.enabled = false;
            }
        }
        else if (GameManager.inBattle)
        {
            if (playerMovement.enabled)
            {
                playerMovement.enabled = false;
            }

            if (!playerBattle.enabled)
            {
                playerBattle.enabled = true;
            }
        }

        AllEnemiesDefeatedCheck();       
    }

    // end the battle
    private void AllEnemiesDefeatedCheck()
    {
        if (GameManager.inBattle)
        {
            if (TurnSystemScript.everyoneLoaded)
            {
                if (TurnSystemScript.selectedCharacter.GetComponent<PlayerBattleScript>().state == PlayerBattleScript.BattleStates.WAIT) 
                {
                    if (turnSystem.enemyGroup.Count == 0)
                    {
                        playerBattle.state = PlayerBattleScript.BattleStates.ENTER_BATTLE;
                        transform.position = returnPos;
                        GetComponentInChildren<Animator>().SetBool("TripleKick", false);
                        GetComponentInChildren<Animator>().SetBool("InBattle", false);
                        turnSystem.EndBattle();
                    }               
                }
            }
        }
    }

    // if the player collides with the enemy, start the battle
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            rb.velocity = Vector3.zero;

            if (!GameManager.inBattle)
            {
                EnemyBattleScript enemy = other.GetComponent<EnemyBattleScript>();

                returnPos = transform.position;

                turnSystem.allCharacterGroup.Add(gameObject);
                turnSystem.allCharacterGroup.Add(other.gameObject);

                // player warp to battle area
                transform.position = playerSpawns[0].transform.position;
                transform.rotation = playerSpawns[0].transform.rotation;
                GetComponentInChildren<Animator>().SetBool("InBattle", true);
                GetComponentInChildren<Animator>().speed = 1f;
                playerBattle.isCreated = false;

                // enemy warp to battle area
                other.transform.position = enemySpawns[0].transform.position;
                other.transform.rotation = enemySpawns[0].transform.rotation;
                enemy.enabled = true;
                enemy.currentHP = enemy.charStats.maxHP;
                other.GetComponentInChildren<Animator>().SetBool("InBattle", true);

                turnSystem.StartBattle();
                GameManager.inBattle = true;
            }
        }
    }
}
