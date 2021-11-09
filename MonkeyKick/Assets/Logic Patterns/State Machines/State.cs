// Merle Roji
// 11/8/21

using System;
using UnityEngine;

namespace MonkeyKick.LogicPatterns
{
    [Serializable]
    public abstract class State
    {
        [SerializeField] protected Action[] actions;
        [SerializeField] protected Action[] entryActions;
        [SerializeField] protected Action[] exitActions;
        [SerializeField] protected Transition[] transitions;

        public virtual Action[] Actions { get => actions; } // return a list of actions to carry out while in state
        public virtual Action[] EntryActions { get => entryActions; } // return a list of actions to carry out while entering the state
        public virtual Action[] ExitActions { get => exitActions; } // return a list of actions to carry out while exiting the state
        public virtual Transition[] Transitions { get => transitions; } // return a list of transitions outgoing from this state
    }
}
