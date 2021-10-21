// Merle Roji
// 10/19/21

using Unity.Collections;
using MonkeyKick.PhysicalObjects.Characters;

namespace MonkeyKick.RPGSystem
{
    [System.Serializable]
    public class TurnClass
    {
        [ReadOnly] public CharacterBattle character; // save the character that this class is saved to
        [ReadOnly] public uint speed; // save the speed of the character
        [ReadOnly] public bool isTurn = false;
        [ReadOnly] public bool wasTurnPrev = false; // is true if the current character had isTurn last turn
    }
}
