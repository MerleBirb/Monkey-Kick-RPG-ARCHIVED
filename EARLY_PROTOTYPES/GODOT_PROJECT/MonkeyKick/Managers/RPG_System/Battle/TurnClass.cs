using Godot;
using System;

namespace Merlebirb.TurnBasedSystem
{
    //===== TURN CLASS =====//
    /*
    4/9/21
    Description: Stores the current information of the object's turn into the turn system

    */

    [Serializable]
    public class TurnClass : Godot.Resource
    {
        public Node character; // stores the character, whether they're an enemy or a player
        public bool isTurn = false;
        public bool wasTurnPrev = false; // if it was this character's turn previously
        public string charName; // store the name of the character
        public int charSpeed; // store the speed of the character
    }

}
