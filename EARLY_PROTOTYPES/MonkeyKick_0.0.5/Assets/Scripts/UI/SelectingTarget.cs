using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingTarget : MonoBehaviour
{
    ////////// MOVE THE SELECTOR ARROW //////////
    // store the turn based system
    TurnSystemScript turnSystem;

    // store the player and enemy battle scripts
    PlayerBattleScript player;
    string playerTag = "Player";
    EnemyBattleScript enemy;
    string enemyTag = "Enemy";

    // store the selector object to instantiate
    public GameObject selector;
    GameObject newSelector;
    bool hasBeenCreated = false;
    public float offsetY = 2.2f;

    // Start is called before the first frame update
    void Start()
    {
        turnSystem = GetComponent<TurnSystemScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnSystemScript.selectedCharacter != null)
        {
            if (TurnSystemScript.selectedCharacter.tag == playerTag)
            {
                player = TurnSystemScript.selectedCharacter.GetComponent<PlayerBattleScript>();

                if (player.state == PlayerBattleScript.BattleStates.CHOOSE_TARGET)
                {
                    if (!hasBeenCreated)
                    {
                        newSelector = Instantiate(selector);
                        hasBeenCreated = true;
                    }

                    if (newSelector != null)
                    {
                        newSelector.transform.position = new Vector3(turnSystem.enemyGroup[player.enemyChosen].transform.position.x,
                            turnSystem.enemyGroup[player.enemyChosen].transform.position.y + offsetY, turnSystem.enemyGroup[player.enemyChosen].transform.position.z);
                    }
                }
                else
                {
                    Destroy(newSelector);
                    hasBeenCreated = false;
                }
            }

            if (TurnSystemScript.selectedCharacter.tag == enemyTag)
            {
                enemy = TurnSystemScript.selectedCharacter.GetComponent<EnemyBattleScript>();

                if (enemy.state == EnemyBattleScript.BattleStates.MOVE_TO_TARGET)
                {
                    if (!hasBeenCreated)
                    {
                        newSelector = Instantiate(selector);
                        hasBeenCreated = true;
                    }

                    if (newSelector != null)
                    {
                        newSelector.transform.position = new Vector3(turnSystem.playerGroup[enemy.playerChosen].transform.position.x,
                            turnSystem.playerGroup[enemy.playerChosen].transform.position.y + offsetY, turnSystem.playerGroup[enemy.playerChosen].transform.position.z);
                    }
                }
                else
                {
                    Destroy(newSelector);
                    hasBeenCreated = false;
                }
            }
        }
    }
}
