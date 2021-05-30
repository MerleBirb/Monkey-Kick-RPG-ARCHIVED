//===== STORE ACTIONS =====//
/*
5/21/21
Description:
- Stores info on the action just taken.

Author: Merlebirb
*/

using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

namespace MonkeyKick.RPGSystem
{
    [Serializable]
    public class StoreActions
    {
        [ReadOnly] public string ActionCharacterName; // store the name of the character who just did an action
        [ReadOnly] public GameObject ActionCharacter; // store the character to last do an action
        [ReadOnly] public List<GameObject> ActionTargets; // store the target(s) of the last action

        public StoreActions()
        {
            ActionCharacterName = "null";
            ActionCharacter = null;
            ActionTargets = new List<GameObject>();
        }
    }
}