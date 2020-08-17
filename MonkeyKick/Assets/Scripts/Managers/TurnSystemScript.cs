using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystemScript : MonoBehaviour
{
    ////////// THE CHARACTER LIST //////////
    // store the playable characters
    public List<GameObject> playerGroup;
    string playerTag = "Player";

    // store the enemies
    public List<GameObject> enemyGroup;
    string enemyTag = "Enemy";

    // combine the player group and the enemy group
    public List<GameObject> allCharacterGroup;
    public List<TurnClass> charList;
    public static bool everyoneLoaded = false;

    // the current character selected
    public static GameObject selectedCharacter;

    ////////// ACTION LIST //////////
    // store the actions done in a list
    public List<StoreActions> actionList;

    // create a counter for how many turns there has been
    public static int turnCounter = 0;

    // called when the battle starts
    public void StartBattle()
    {
        // functions to get the battle started
        FillBattleList();
        SetBattleOrder();
        selectedCharacter = charList[0].character; // the first character in the list is the selected character
        ResetTurns();
        SetBattlePosition();
    }

    // called when battle ends
    public void EndBattle()
    {
        for (int i = 0; i < charList.Count; i++)
        {
            charList[i].isTurn = false;
            charList[i].wasTurnPrev = false;    
        }

        selectedCharacter = null;
        actionList.Clear();
        charList.Clear();
        allCharacterGroup.Clear();
        playerGroup.Clear();
        enemyGroup.Clear();

        everyoneLoaded = false;
        GameManager.inBattle = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTurns();
    }

    // resets the turns after everyone has gone
    void ResetTurns()
    {
        for(int i = 0; i < charList.Count; i++)
        {
            if(i == 0)
            {
                charList[i].isTurn = true;
                charList[i].wasTurnPrev = false;
            }
            else
            {
                charList[i].isTurn = false;
                charList[i].wasTurnPrev = false;
            }

            if (charList[i] == null)
            {
                charList.Remove(charList[i]);
            }
        }
    }

    // cycles through the turns and checks to see who's turn it is
    void UpdateTurns()
    {
        for(int i = 0; i < charList.Count; i++)
        {
            if(!charList[i].wasTurnPrev)
            {
                BattleUIScript.hasUpdatedPlayerBattle = false;
                charList[i].isTurn = true;
                break;
            }
            else if((i == charList.Count - 1) && (charList[i].wasTurnPrev))
            {
                SetBattleOrder();
                ResetTurns();
                actionList.Clear();
                turnCounter++;
            }
        }

        for (int i = 0; i < charList.Count; i++)
        {
            if (tag == playerTag)
            {
                if (selectedCharacter.transform.position == selectedCharacter.GetComponent<PlayerBattleScript>().battlePos)
                {
                    if (charList[i].isTurn)
                    {
                        selectedCharacter = charList[i].character;
                    }
                }
            }
            else if (tag == enemyTag)
            {
                if (selectedCharacter.transform.position == selectedCharacter.GetComponent<EnemyBattleScript>().battlePos)
                {
                    if (charList[i].isTurn)
                    {
                        selectedCharacter = charList[i].character;
                    }
                }
            }
        }
    }

    // fill out the list of total characters both from the player party and the enemy party
    void FillBattleList()
    {
        for(int i = 0; i < allCharacterGroup.Count; i++)
        {
            if (allCharacterGroup[i].tag == playerTag)
            {
                charList.Add(allCharacterGroup[i].GetComponent<PlayerBattleScript>().turnClass);
                playerGroup.Add(allCharacterGroup[i]);
            }
            else if (allCharacterGroup[i].tag == enemyTag)
            {
                charList.Add(allCharacterGroup[i].GetComponent<EnemyBattleScript>().turnClass);
                enemyGroup.Add(allCharacterGroup[i]);
            }                      
        }       

        for (int i = 0; i < charList.Count; i++)
        {
            charList[i].character = allCharacterGroup[i];

            if (charList[i].character.tag == playerTag)
            {
                charList[i].charSpeed = charList[i].character.GetComponent<PlayerBattleScript>().charStats.speed;
                charList[i].charName = charList[i].character.GetComponent<PlayerBattleScript>().charStats.name;
            }
            else if(charList[i].character.tag == enemyTag)
            {
                charList[i].charSpeed = charList[i].character.GetComponent<EnemyBattleScript>().charStats.speed;
                charList[i].charName = charList[i].character.GetComponent<EnemyBattleScript>().charStats.name;
            }
        }

        if (playerGroup.Count + enemyGroup.Count == charList.Count)
        {
            everyoneLoaded = true;
            Debug.Log("LOADED BATTLE");
        }
    }

    // set the battle position
    void SetBattlePosition()
    {
        for (int i = 0; i < playerGroup.Count; i++)
        {
            playerGroup[i].GetComponent<PlayerBattleScript>().battlePos = 
                new Vector3((float)System.Math.Round(playerGroup[i].transform.position.x, 1),
                (float)System.Math.Round(playerGroup[i].transform.position.y, 1), (float)System.Math.Round(playerGroup[i].transform.position.z, 1));
        }

        for (int i = 0; i < enemyGroup.Count; i++)
        {
            enemyGroup[i].GetComponent<EnemyBattleScript>().battlePos =
                new Vector3((float)System.Math.Round(enemyGroup[i].transform.position.x, 1),
                (float)System.Math.Round(enemyGroup[i].transform.position.y, 1), (float)System.Math.Round(enemyGroup[i].transform.position.z, 1));
        }
    }

    // sort battle turn order by the speed stat of the character
    void SetBattleOrder()
    {
        charList.Sort((a, b) =>
        {
            var speedA = a.charSpeed;
            var speedB = b.charSpeed;

            // Sort the speeds
            return speedA < speedB ? 1 : (speedA == speedB ? 0 : -1);
        });
    }

    // store the actions every time one is done
    public void CollectAction(StoreActions input)
    {
        actionList.Add(input);
    }

    // Damage calculator
    public int DealDamage(int basePower, int attackerAtk, int attackerLvl, int defenderDef,
        int defenderLvl, int clampValue)
    {
        int damage = Mathf.Clamp(basePower * ((attackerAtk + attackerLvl) / (defenderDef + defenderLvl)), 1, clampValue);

        return damage;
    }
}

[System.Serializable]
public class TurnClass
{
    // store the character
    public GameObject character;

    // is it the specific character's turn?
    public bool isTurn = false;

    // was it the specific character's turn previously?
    public bool wasTurnPrev = false;

    // storing the name of the character
    public string charName;

    // storing the speed stat of the character
    public int charSpeed;
}
