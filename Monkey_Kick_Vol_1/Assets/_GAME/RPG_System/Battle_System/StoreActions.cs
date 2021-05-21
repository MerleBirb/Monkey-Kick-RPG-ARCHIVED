//===== STORE ACTIONS =====//
/*
5/21/21
Description:
- Stores info on the action just taken.

*/

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StoreActions
{
    public string actionCharacterName; // store the name of the character who just did an action
    public GameObject actionCharacter; // store the character to last do an action
    public List<GameObject> actionTargets; // store the target(s) of the last action

    public StoreActions()
    {
        actionCharacterName = "null";
        actionCharacter = null;
        actionTargets = new List<GameObject>();
    }
}
