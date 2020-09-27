using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIScript : MonoBehaviour
{
    ////////// THE BATTLE UI //////////
    // store turn based system
    public TurnSystemScript turnSystem;

    // store the battle UI
    public List<GameObject> battleMenuUI;
    private List<GameObject> unselectedBattleMenu;
    public GameObject arrow;
    string battleMenuTag = "BattleMenuUI";

    // call the current player battle script
    PlayerBattleScript playerBattle;
    string playerTag = "Player";
    public static bool hasUpdatedPlayerBattle = false;

    // Start is called before the first frame update
    void Start()
    {
        battleMenuUI.AddRange(GameObject.FindGameObjectsWithTag(battleMenuTag));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.inBattle)
        {
            CurrentPlayerBattle();
            MoveArrow();
        }
    }

    // keep updating the current player battle script
    void CurrentPlayerBattle()
    {
        if(!hasUpdatedPlayerBattle)
        {
            if (TurnSystemScript.selectedCharacter.tag == playerTag)
            {
                playerBattle = TurnSystemScript.selectedCharacter.GetComponent<PlayerBattleScript>();
                hasUpdatedPlayerBattle = true;
            }
        }
    }

    // move the arrow relative to the player's menu choice
    void MoveArrow()
    {
        if (playerBattle.state == PlayerBattleScript.BattleStates.SELECT_ACTION)
        {
            for (int i = 0; i < battleMenuUI.Count; i++)
            {
                if (!battleMenuUI[i].activeSelf)
                {
                    battleMenuUI[i].SetActive(true);
                }
            }

            battleMenuUI[playerBattle.actionSelect].GetComponent<Text>().color = Color.white;
            unselectedBattleMenu = battleMenuUI.Where((t) => t != battleMenuUI[playerBattle.actionSelect]).ToList();

            if (unselectedBattleMenu != null)
            {
                for (int e = 0; e < unselectedBattleMenu.Count; e++)
                {
                    unselectedBattleMenu[e].GetComponent<Text>().color = Color.gray;
                }
            }
        }
        else
        {
            for (int i = 0; i < battleMenuUI.Count; i++)
            {
                if (battleMenuUI[i].activeSelf)
                {
                    battleMenuUI[i].SetActive(false);
                }
            }
        }
    }
}
