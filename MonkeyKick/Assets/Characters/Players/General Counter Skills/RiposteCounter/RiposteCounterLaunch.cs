// Merle Roji
// 11/14/21

using UnityEngine.InputSystem;
using MonkeyKick.RPGSystem;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class RiposteCounterLaunch : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private string _targetState; // the target state that this state will transition to
        private InputAction _button; // store the button being pressed

        public RiposteCounterLaunch(Skill skill, string targetState, InputAction button)
        {
            _skill = skill;
            _targetState = targetState;
            _button = button;
        }

        public override bool Execute()
        {
            if (!_button.triggered)
            {
                _skill.SetState(_targetState);
                return true;
            }

            return false;
        }
    }
}
