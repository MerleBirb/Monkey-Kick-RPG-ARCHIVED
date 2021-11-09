// Merle Roji
// 11/9/21

using UnityEngine;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public abstract class StateAction
    {
        public abstract bool Execute(); // bool because if an action is running you most likely don't want to run another one
    }
}
