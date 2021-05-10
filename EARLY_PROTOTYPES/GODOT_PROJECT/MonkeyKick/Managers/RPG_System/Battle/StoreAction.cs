using Godot;
using System;

namespace Merlebirb.TurnBasedSystem
{
    //===== STORE ACTIONS =====//
    /*
    4/9/21
    Description: Stores the information about the action just taken in the turn by the current selected character in the turn system
    
    */


    [Serializable]
    public class StoreAction : Godot.Resource
    {
        public string performedCharacterName; // store the name of the last character that performed an action
        public Node performedCharacter; // store the last character that performed an action
        public Node performedTarget; // store the target of said action
    }

}
