// Merle Roji 7/19/22

using System.Collections.Generic;
using UnityEngine;

namespace MonkeyKick
{
    public abstract class ScriptableObjectStateMachine : ScriptableObject
    {
        #region STATE PATTERN VARIABLES

        protected State _currentState;
        protected Dictionary<string, State> _allStates = new Dictionary<string, State>();

        protected State GetState(string stateID)
        {
            _allStates.TryGetValue(stateID, out State returnValue);
            return returnValue;
        }

        public void SetState(string targetID)
        {
            State targetState = GetState(targetID);
            if (targetState == null) Debug.LogError(targetID + " was not found."); // if the targetID wasnt found 
            _currentState = targetState;
        }

        #endregion

        #region STATE PATTERN METHODS

        public void FixedTick()
        {
            if (_currentState != null) _currentState.FixedTick(); // set the current state to execute its actions in the FixedUpdate() loop
        }

        public void Tick()
        {
            if (_currentState != null) _currentState.Tick(); // set the current state to execute its actions in the Update() loop
        }

        #endregion
    }
}
