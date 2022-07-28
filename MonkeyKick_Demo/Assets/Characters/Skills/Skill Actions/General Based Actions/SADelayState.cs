// Merle Roji 7/28/22

using UnityEngine;

namespace MonkeyKick.Skill
{
    public class SADelayState : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private string _targetState; // the target state that this state will transition to
        private float _time; // time that the state will be delayed by
        private float _currentTime; // timer

        public SADelayState(Skill skill, string targetState, float delayTime = 0f)
        {
            _skill = skill;
            _targetState = targetState;
            _time = delayTime;
        }

        public override bool Execute()
        {
            if (_currentTime < _time)
            {
                _currentTime += Time.deltaTime;
            }
            else
            {
                _skill.SetState(_targetState);
                _currentTime = 0f;
                return true;
            }

            return false;
        }
    }
}

