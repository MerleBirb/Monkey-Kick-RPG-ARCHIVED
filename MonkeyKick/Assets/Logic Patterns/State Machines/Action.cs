// Merle Roji
// 11/8/21

using UnityEngine;

namespace MonkeyKick.LogicPatterns
{
    [System.Serializable]
    public abstract class Action :  ScriptableObject
    {
        public abstract void DoAction(); // Every action must have this function, action logic goes in here
    }
}


