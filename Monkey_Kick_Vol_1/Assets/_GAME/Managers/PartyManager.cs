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
    [HideInInspector] public PlayerController mainPlayer; // the current character you are playing as

    protected readonly List<PlayableController> playerParty; // private player party
    public readonly ReadOnlyCollection<PlayableController> PlayerParty; // readable player party

    private PartyManager()
    {
        playerParty = new List<PlayableController>();
        PlayerParty = playerParty.AsReadOnly();
    }
    
    private void Awake()
    {  
        CheckIfFirstPlayerExists();
    }

    private void CheckIfFirstPlayerExists() // needs this to load the player
    {
        if (firstPlayer != null)
        {
            mainPlayer = firstPlayer;
            playerParty.Add(mainPlayer);
            Debug.Log("First Player: " + mainPlayer.stats.characterName);
        }
        else // (if firstPlayer == null)
        {
            Debug.LogError(">>>ERROR: No First Player Exists! Make sure to set them in the inspector.");
        }
    }

    public void TemporarilySavePlayerStats(PlayableController player)
    {
        mainPlayer.stats = player.stats;
    }

    public void TemporarilyLoadPlayerStats(PlayableController player)
    {
        player.stats = mainPlayer.stats;
    }

    public void ChangeMainPlayer(PlayerController player)
    {
        mainPlayer = player;
        mainPlayer.stats = player.stats;
    }

    public bool SearchIfPartyMemberExists(PlayableController searchedPartyMember)
    {
        return playerParty.Contains(searchedPartyMember);
    }

    public PlayableController SearchAndGetPartyMember(PlayableController searchedPartyMember)
    {
        PlayableController partyMember = null; // if the party member doesn't exist, return null

        for (int i = 0; i < playerParty.Count; i++)
        {
            if (playerParty[i].stats.characterName.Equals(searchedPartyMember.stats.characterName))
            {
                partyMember = playerParty[i];
            }
        }

        return partyMember;
    }

}
