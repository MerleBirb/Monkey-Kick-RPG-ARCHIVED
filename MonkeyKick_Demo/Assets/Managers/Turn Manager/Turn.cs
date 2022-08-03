// Merle Roji 7/12/22

using Unity.Collections;
using MonkeyKick.Characters;

namespace MonkeyKick.Managers.TurnSystem
{
    /// <summary>
    /// Saves the information of the battle turn.
    /// 
    /// Notes:
    /// 
    /// </summary>
    [System.Serializable]
    public class Turn
    {
        [ReadOnly] public Character Character;
        [ReadOnly] public int Speed; // saves the speed from the character stats
        [ReadOnly] public bool IsTurn = false;
        [ReadOnly] public bool WasTurnPrev = false; // true if the character had 'IsTurn' true last turn
        [ReadOnly] public bool IsDead = false;
    }
}