// Merle Roji 8/1/22

using UnityEngine;
using UnityEngine.InputSystem;

namespace MonkeyKick.Skills
{
    public class SARiposteCounterInput : StateAction
    {
        private SKRiposteCounter _skill; // store the state machine of the skill
        private string _targetState; // the target state that this state will transition to
        private InputAction _button; // store the button being pressed
        private float _limitTime; // the limit for the timner
        private bool _hasAttemptedHolding = false; // if the player has held the button enough or not
        private string _windUpAnim;
        private string _stanceAnim;

        public SARiposteCounterInput(SKRiposteCounter skill, string targetState, InputAction button, string windUpAnim, string stanceAnim, float limitTime)
        {
            _skill = skill;
            _targetState = targetState;
            _button = button;
            _windUpAnim = windUpAnim;
            _stanceAnim = stanceAnim;
            _limitTime = limitTime;

            _hasAttemptedHolding = false;
        }

        public override bool Execute()
        {
            if (_button.IsPressed()) // check to see if it's held
            {
                _skill.CounterTimer += Time.deltaTime;
                _hasAttemptedHolding = true;

                AnimationQoL.ChangeAnimation(_skill.ActorAnim, _windUpAnim);

                return false;
            }

            if (!_button.IsPressed() && _hasAttemptedHolding)
            {
                if (_skill.CounterTimer >= _limitTime)
                {
                    _skill.SetState(_targetState);
                    _hasAttemptedHolding = false;
                    _skill.CounterTimer = 0f;

                    return true;
                }
                else
                {
                    AnimationQoL.ChangeAnimation(_skill.ActorAnim, _stanceAnim);
                    _hasAttemptedHolding = false;
                    _skill.CounterTimer = 0f;

                    return true;
                }
            }

            return false;
        }
    }
}
