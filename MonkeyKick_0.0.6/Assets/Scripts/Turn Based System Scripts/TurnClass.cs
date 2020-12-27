using UnityEngine;

namespace Merlebird.TurnBasedSystem
{
    [System.Serializable]
    public class TurnClass
    {
        /// TURN CLASS ///
        /// Class that stores important aspects of every object enterting the turn system

        // the character themselves
        [HideInInspector]
        public GameObject character;

        // the name of the character
        [HideInInspector]
        public string charName;

        // boolean which tells if it is the character's turn or not
        [HideInInspector]
        public bool isTurn = false, wasTurnPrev = false;

        // store the speed of the character to be used in the sorting turn order calculation
        [HideInInspector]
        public int charSpeed;

        // store the battle position of the character to return them to their respective spot
        [HideInInspector]
        public Vector3 battlePos;
    }
}

