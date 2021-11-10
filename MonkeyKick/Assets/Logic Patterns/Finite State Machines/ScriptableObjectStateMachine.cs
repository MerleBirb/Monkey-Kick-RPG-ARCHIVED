// Merle Roji
// 11/9/21

using System.Collections.Generic;
using UnityEngine;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public abstract class ScriptableObjectStateMachine : ScriptableObject
    {
        #region STATE PATTERN VARIABLES

        protected State currentState;
        protected Dictionary<string, State> allStates = new Dictionary<string, State>();

        protected State GetState(string stateID)
        {
            allStates.TryGetValue(stateID, out State returnValue);
            return returnValue;
        }

        public void SetState(string targetID)
        {
            State targetState = GetState(targetID);
            if (targetState == null) Debug.LogError(targetID + " was not found."); // if the targetID wasnt found 
            currentState = targetState;
        }

        #endregion

        #region STATE PATTERN METHODS

        public void FixedTick()
        {
            if (currentState != null) currentState.FixedTick(); // set the current state to execute its actions in the FixedUpdate() loop
        }

        public void Tick()
        {
            if (currentState != null) currentState.Tick(); // set the current state to execute its actions in the Update() loop
        }

        #endregion
    }
}
