// Merle Roji 7/19/22

using System.Collections.Generic;

namespace MonkeyKick
{
    public class State
    {
        protected StateAction[] _fixedUpdateActions; // actions that run in the FixedUpdate() function
        protected StateAction[] _updateActions; // actions that run in the Update() function
        protected bool forceSkip = false;

        // Constructor
        public State(StateAction[] fixedUpdateActions, StateAction[] updateActions)
        {
            _fixedUpdateActions = fixedUpdateActions;
            _updateActions = updateActions;
        }

        public void FixedTick()
        {
            ExecuteActionsArray(_fixedUpdateActions);
        }

        public void Tick()
        {
            ExecuteActionsArray(_updateActions);
            forceSkip = false; // force skip happens in normal tick because FixedUpdate() has priority over Update()
        }

        protected void ExecuteActionsArray(StateAction[] array)
        {
            if (array == null) return; // if there is no array, don't run

            for (int i = array.Length; --i >= 0;)
            {
                if (forceSkip) break; // skip if action is already executing
                forceSkip = array[i].Execute();
            }
        }
    }
}