using UnityEngine;

namespace Merlebird.TurnBasedSystem
{
    [System.Serializable]
    public class StoreActions
    {
        /// STORE ACTIONS ///
        /// A small class that stores specific info about some characters

        // the name of the character that is attacking
        public string attackerName;

        // the attacker themselves
        public GameObject attacker;

        // the target of the attacker
        public GameObject attackerTarget;
    }
}
