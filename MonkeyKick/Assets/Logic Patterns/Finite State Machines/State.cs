// Merle Roji
// 11/9/21

using System.Collections.Generic;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class State
    {
        
        protected StateAction[] fixedActions; // actions that run in the FixedUpdate() function
        protected StateAction[] updateActions; // actions that run in the Update() function
        protected bool forceSkip;

        // Constructor
        public State(StateAction[] fixedActions, StateAction[] updateActions)
        {
            this.fixedActions = fixedActions;
            this.updateActions = updateActions;
        }

        public void FixedTick()
        {
            ExecuteActionsArray(fixedActions);
        }

        public void Tick()
        {
            ExecuteActionsArray(updateActions);
            forceSkip = false; // force skip happens in normal tick because FixedUpdate() has priority over Update()
        }

        protected void ExecuteActionsArray(StateAction[] array)
        {
            if (array == null) return; // if there is no array, don't run

            for (int i = 0; i < array.Length; i++)
            {
                if (forceSkip) break; // skip if action is already executing
                forceSkip = updateActions[i].Execute();
            }
        }
        
    }
}
