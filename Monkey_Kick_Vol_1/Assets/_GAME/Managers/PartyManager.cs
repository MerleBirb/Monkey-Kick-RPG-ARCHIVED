//===== PARTY MANAGER =====//
/*
5/19/21
Description:
- Handles saving and loading of player party data.

*/

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public PlayerController firstPlayer; // the first character you play as
    public PlayerController mainPlayer; // the current character you are playing as

    protected readonly List<PlayableController> playerParty; // private player party
    public readonly ReadOnlyCollection<PlayableController> PlayerParty; // readable player party

    private PartyManager()
    {
        playerParty = new List<PlayableController>();
        PlayerParty = playerParty.AsReadOnly();
    }
    
    private void Awake()
    {  
        CheckIfFirstPlayerExists(firstPlayer);
    }

    private void CheckIfFirstPlayerExists(PlayerController player) // needs this to load the player
    {
        if (player != null)
        {
            playerParty.Add(firstPlayer);
            mainPlayer = (PlayerController)playerParty[0];
            Debug.Log("First Player: " + firstPlayer.stats.characterName);
        }
        else // (if firstPlayer == null)
        {
            Debug.LogError(">>>ERROR: No First Player Exists! Make sure to set them in the inspector.");
        }
    }

}
