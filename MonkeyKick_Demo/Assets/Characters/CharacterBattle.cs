// Merle Roji 7/12/22

using UnityEngine;
using MonkeyKick.Managers.TurnSystem;

namespace MonkeyKick.Characters
{
    /// <summary>
    /// Handles battle logic for characters.
    /// 
    /// Notes:
    /// 
    /// </summary>
    public abstract class CharacterBattle : MonoBehaviour
    {
        // turn
        protected Turn _turn;
        public Turn Turn
        {
            get => _turn;
            set => _turn = value;
        }

        protected bool _isTurn = false;
    }
}

