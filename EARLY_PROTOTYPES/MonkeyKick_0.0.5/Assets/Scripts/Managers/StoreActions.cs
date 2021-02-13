using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoreActions
{
    ////////// STORE THE ACTIONS MADE //////////
    // store the name of the last attacker
    public string attackerName;

    // storing the object that is the attacker
    public GameObject attacker;

    // the target of the last attacker
    public GameObject attackerTarget;
}
