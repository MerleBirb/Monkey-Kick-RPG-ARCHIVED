//===== TURN CLASS =====//
/*
5/21/21
Description:
- Holds the information on the current turn.

*/

using System;
using UnityEngine;

[Serializable]
public class TurnClass
{
    public GameObject character;
    public bool isTurn = false;
    public bool wasTurnPrev = false; // is true if the current character had isTurn last turn
    public string charName; // saves the character name

    public CharacterStat charSpeed; // saves the speed of the character
}
