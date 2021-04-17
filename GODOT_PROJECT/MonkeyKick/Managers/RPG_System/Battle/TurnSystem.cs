using Godot;
using System.Collections.Generic;
using Merlebirb.TurnBasedSystem;
using Merlebirb.Managers;

//===== TURN BASED BATTLE SYSTEM =====//
/*
4/9/21
Description: Handles all the logic of sorting by speed stat, turn order, turn based, etc. SUPER important.
Notes: 
- might rewrite later if inefficient.

*/

public class TurnSystem : Node
{
    #region character lists

    public static List<Node> playerList = new List<Node>();
    public static List<Node> enemyList = new List<Node>();
    public static List<Node> allCharacterList = new List<Node>();
    public static Node selectedCharacter; // character who's turn is active

    #endregion

    #region storing turns

    public static List<TurnClass> charList; // list of quick and important character info
    public static List<StoreAction> actionList; // store actions executed on turn

    #endregion

    #region misc

    public static bool everyoneLoaded = false;
    public static int turnCounter = 0;

    #endregion

    public static void StartBattle(List<Node> enemyParty)
    {
        GD.Print ("Starting Battle Sequence...");

        FillCharacterLists(enemyParty); // fills the all character list
        FillBattleList();
        SetBattleOrder();
        //selectedCharacter = charList[0].character; // the first character in the list is the selected character
        ResetTurns();
        SetBattlePosition();

        GD.Print ("Loaded Battle.");
    }

    public static void EndBattle()
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

        turnCounter = 0;

        everyoneLoaded = false;
        GameManager.ChangeGameState(GameStates.OVERWORLD);
    }

    public static void UpdateTurns()
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

    public static void FillCharacterLists(List<Node> enemyParty)
    {
        allCharacterList.AddRange(GameManager.playerParty);
        GD.Print("Added Players to character list.");
        allCharacterList.AddRange(enemyParty);
        GD.Print("Added Enemies to character list.");
    }

    private static void FillBattleList()
    {
        //if (playerList.Count + enemyList.Count == charList.Count)
        //{
        //    everyoneLoaded = true;
        //    GD.Print("LOADED BATTLE");
        //}
    }

    private static void SetBattleOrder()
    {

    }

    private static void SetBattlePosition()
    {

    }

    private static void ResetTurns()
    {
        
    }
}
