//===== CHARACTER BATTLE =====//
/*
5/23/21
Description:
- Abstract class to hold general information for all character logic in the battle game state.

*/

using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    [SerializeField] private GameStateData Game;

    public CharacterInformation stats;

    public void Update()
    {
        if (!Game.CompareGameState(GameStates.Battle)) { this.enabled = false; }
    }
}
