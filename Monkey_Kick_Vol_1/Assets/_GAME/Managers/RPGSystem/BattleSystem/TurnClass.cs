//===== TURN CLASS =====//
/*
5/21/21
Description:
- Holds the information on the current turn.

Author: Merlebirb
*/

using System;
using UnityEngine;

[Serializable]
public class TurnClass
{
    [ReadOnly] public TurnSystem turnSystem;
    [ReadOnly] public CharacterBattle character;
    [ReadOnly] public bool isTurn = false;
    [ReadOnly] public bool wasTurnPrev = false; // is true if the current character had isTurn last turn
    [ReadOnly] public string charName; // saves the character name
    [ReadOnly] public CharacterStat charSpeed; // saves the speed of the character
}
