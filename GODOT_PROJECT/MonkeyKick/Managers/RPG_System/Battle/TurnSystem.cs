using Godot;
using System.Collections.Generic;
using Merlebirb.TurnBasedSystem;
using Merlebirb.Managers;

//===== TURN BASED BATTLE SYSTEM =====//
/*
4/9/21
Description: Handles all the logic of sorting by speed stat, turn order, turn based, etc.

*/

public class TurnSystem : Node
{
    public List<Node> playerList = new List<Node>();
    private string playerTag = "Player";
    public List<Node> enemyList = new List<Node>();
    private string enemyTag = "Enemy";
    public List<PackedScene> allCharacterList;
    public static Node selectedCharacter;

    public List<TurnClass> charList;
    public List<StoreAction> actionList;

    public static bool everyoneLoaded = false;
    public static int turnCounter = 0;

    public void StartBattle()
    {
        FillCharacterLists();
        FillBattleList();
        SetBattleOrder();
        selectedCharacter = charList[0].character; // the first character in the list is the selected character
        ResetTurns();
        SetBattlePosition();
    }

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
        allCharacterList.Clear();
        playerList.Clear();
        enemyList.Clear();

        everyoneLoaded = false;
        GameManager.ChangeGameState(GameStates.OVERWORLD);
    }

    public void UpdateTurns()
    {
        for (int i = 0; i < charList.Count; i++)
        {
            if (!charList[i].wasTurnPrev)
            {
                charList[i].isTurn = true;
                break;
            }
            else if ((i == charList.Count - 1) && (charList[i].wasTurnPrev))
            {
                SetBattleOrder();
                ResetTurns();
                actionList.Clear();
                turnCounter++;
            }
        }

        //for (int i = 0; i < charList.Count; i++)
        //{
        //    if ((string)selectedCharacter.GetMeta("Type") == playerTag)
        //    {
        //        if (selectedCharacter.transform.position == selectedCharacter.GetComponent<PlayerBattleScript>().battlePos)
        //        {
        //            if (charList[i].isTurn)
        //            {
        //                selectedCharacter = charList[i].character;
        //            }
        //        }
        //    }
        //    else if ((string)selectedCharacter.GetMeta("Type") == enemyTag)//
        //    {
        //        if (selectedCharacter.transform.position == selectedCharacter.GetComponent<EnemyBattleScript>().battlePos)
        //        {
        //            if (charList[i].isTurn)
        //            {
        //                selectedCharacter = charList[i].character;
        //            }
        //        }
        //    }
        //}
    }

    public void FillCharacterLists()
    {
        
    }

    private void FillBattleList()
    {
        //if (playerList.Count + enemyList.Count == charList.Count)
        //{
        //    everyoneLoaded = true;
        //    GD.Print("LOADED BATTLE");
        //}
    }

    private void SetBattleOrder()
    {

    }

    private void SetBattlePosition()
    {

    }

    private void ResetTurns()
    {
        
    }
}
