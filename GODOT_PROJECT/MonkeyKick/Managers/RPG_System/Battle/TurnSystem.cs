using Godot;
using System.Collections.Generic;
using Merlebirb.TurnBasedSystem;

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
    public List<Node> allCharacterList;
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

    }

    public void UpdateTurns()
    {

    }

    private void FillCharacterLists()
    {

    }

    private void FillBattleList()
    {

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
