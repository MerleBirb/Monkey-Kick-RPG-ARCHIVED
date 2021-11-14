// Merle Roji
// 11/14/21

using UnityEngine;
using UnityEngine.InputSystem;
using MonkeyKick.RPGSystem;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class RiposteCounterInput : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private string _targetState; // the target state that this state will transition to
        private string _previousState; // if you let go of the button
        private InputAction _button; // store the button being pressed
        private float _limitTime; // the limit for the timner
        private float _currentTime; // the current time on the timer

        public RiposteCounterInput(Skill skill, string targetState, string previousState, InputAction button, float limitTime)
        {
            _skill = skill;
            _targetState = targetState;
            _previousState = previousState;
            _button = button;
            _limitTime = limitTime;
            _currentTime = 0f;
        }

        public override bool Execute()
        {
            if (_button.triggered)
            {
                _currentTime += Time.deltaTime;
            }
            else
            {
                if (_currentTime >= _limitTime)
                {
                    _skill.SetState(_targetState);
                    return true;
                }
                else
                {
                    _skill.SetState(_previousState);
                    return true;
                }
            }

            return false;
        }
    }
}
