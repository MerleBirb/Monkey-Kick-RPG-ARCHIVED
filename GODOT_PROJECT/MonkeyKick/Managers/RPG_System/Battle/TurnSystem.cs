using Godot;
using System.Collections.Generic;
using Merlebirb.TurnBasedSystem;
using Merlebirb.Managers;
using Merlebirb.QualityOfLife;
using Merlebirb.Tag;

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
    public static List<Node> enemyParty = new List<Node>();
    public static List<Node> allCharacterList = new List<Node>();
    public static Node selectedCharacter; // character who's turn is active

    #endregion

    #region storing turns

    public static List<TurnClass> charList = new List<TurnClass>(); // list of quick and important character info
    public static List<StoreAction> actionList; // store actions executed on turn

    #endregion

    #region misc

    public static bool everyoneLoaded = false;
    public static int turnCounter = 0;

    #endregion

    public static void LoadBattle(List<Node> ep)
    {   
        if (everyoneLoaded) { everyoneLoaded = false; };
        //ResetCharacterLists();
        enemyParty = ep;
        GD.Print("Enemy party size: " + enemyParty.Count);
    }

    public static void StartBattle()
    {
        GD.Print ("Starting Battle Sequence...");
   
        FillCharacterLists(); // fills the character lists 
        FillBattleList(); // sets the player list and enemy list and fill out turn class information
        SetBattleOrder(); // sorts the battle order by speed
        SpawnCharacters(); // spawn the characters into the scene
        selectedCharacter = charList[0].character; // the first character in the list is the selected character
        ResetTurns();

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

        for (int e = 0; e < charList.Count; e++)
        {
            if (charList[e].isTurn)
            {
                selectedCharacter = charList[e].character;
            }
        }
    }

    public static void FillCharacterLists()
    {
        allCharacterList.AddRange(GameManager.playerParty);
        allCharacterList.AddRange(enemyParty);
        GD.Print("All character list count: " + allCharacterList.Count);

        for (int i = 0; i < allCharacterList.Count; i++)
        {
            // make new tag system
            if(allCharacterList[i].HasTag("Player"))
            {     
                GD.Print("Adding Player to player list...");
                charList.Add(allCharacterList[i].GetNode<PlayerBattle>("Battle").turnClass);
                playerList.Add(allCharacterList[i]);
                GD.Print("Added " + allCharacterList[i].Name + " to player list.");
            }
            else if (allCharacterList[i].HasTag("Enemy"))
            {
                GD.Print("Adding Enemy to enemy list...");
                charList.Add(allCharacterList[i].GetNode<EnemyBattle>("Battle").turnClass);
                enemyList.Add(allCharacterList[i]);
                GD.Print("Added " + allCharacterList[i].Name + " to enemy list.");
            }
        }
        
        GD.Print("CharList count: " + charList.Count);
    }

    private static void FillBattleList()
    {
        for (int i = 0; i < charList.Count; i++)
        {
            charList[i].character = allCharacterList[i]; // save the character variable as itself

            if (charList[i].character.HasTag("Player"))
            {
                charList[i].charName = charList[i].character.GetNode<PlayerBattle>("Battle").stats.name;
                charList[i].charSpeed = (int)charList[i].character.GetNode<PlayerBattle>("Battle").stats.speed.BaseValue;
            }
            else if (charList[i].character.HasTag("Enemy"))
            {
                charList[i].charName = charList[i].character.GetNode<EnemyBattle>("Battle").stats.name;
                charList[i].charSpeed = (int)charList[i].character.GetNode<EnemyBattle>("Battle").stats.speed.BaseValue;
            }
        }

        if (playerList.Count + enemyList.Count == charList.Count)
        {
            everyoneLoaded = true;
            GD.Print("Every character is loaded.");
        }
    }

    private static void SetBattleOrder()
    {
        // compares speed of characters in the character list and sorts them
        charList.Sort((a, b) =>
        {
            var speedA = a.charSpeed;
            var speedB = b.charSpeed;

            // Sort the speeds
            return speedA < speedB ? 1 : (speedA == speedB ? 0 : -1);
        });

        GD.Print("Turn order: ");
        for (int i = 0; i < charList.Count; i++)
        {
            GD.Print(charList[i].charName);
        }
    }

    private static void SpawnCharacters()
    {
        GD.Print("Spawning characters...");

        var playerSpawns = TagSystem.AllObjectsForTag("PlayerSpawn");
        var enemySpawns = TagSystem.AllObjectsForTag("EnemySpawn");

        for(int i = 0; i < playerList.Count; i++)
        {
            playerSpawns[i].AddChild(playerList[i]);
        }

        GD.Print("Spawned all players.");

        for (int e = 0; e < enemyList.Count; e++)
        {
            enemySpawns[e].AddChild(enemyList[e]);
        }

        GD.Print("Spawned all enemies.");
    }

    private static void ResetTurns()
    {
        for (int i = 0; i < charList.Count; i++)
        {
            if (i == 0)
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

    public static void CollectAction(StoreAction input)
    {
        actionList.Add(input);
    }

    public static void ResetCharacterLists()
    {
        allCharacterList.Clear();
        charList.Clear();
        playerList.Clear();
        enemyList.Clear();
        enemyParty.Clear();
    }
}
