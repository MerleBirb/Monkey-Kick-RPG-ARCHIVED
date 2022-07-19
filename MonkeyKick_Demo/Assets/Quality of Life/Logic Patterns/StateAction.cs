// Merle Roji 7/19/22

using UnityEngine;

namespace MonkeyKick
{
    public abstract class StateAction
    {
        public abstract bool Execute(); // bool because if an action is running you most likely don't want to run another one
    }
}
