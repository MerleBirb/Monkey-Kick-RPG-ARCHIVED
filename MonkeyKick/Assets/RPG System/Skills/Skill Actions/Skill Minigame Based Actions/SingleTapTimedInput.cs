// Merle Roji
// 11/9/21

using UnityEngine;
using MonkeyKick.PhysicalObjects.Characters;
using MonkeyKick.RPGSystem;
using MonkeyKick.QualityOfLife;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class SingleTapTimedInput : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private PlayerBattle _playerActor; // a player actor who can press buttons
        private string _targetState; // the target state that this state will transition to
        private float _limitTime; // the limit for the timner
        private float _currentTime; // the current time on the timer
        private float[] _timeChecks; // the timestamps on which your rating changes


        public SingleTapTimedInput(Skill skill, string targetState, float limitTime, float[] timeChecks)
        {
            _skill = skill;
            _playerActor = skill.actor.GetComponent<PlayerBattle>();
            _targetState = targetState;
            _limitTime = limitTime;
            _currentTime = limitTime;
            _timeChecks = timeChecks;
        }

        public override bool Execute()
        {
            if (_currentTime >= 0f)
            {
                _currentTime -= Time.deltaTime;

                if (_playerActor.pressedButtonSouth)
                {
                    _skill.currentRating = SkillQoL.TimedButtonPress(_currentTime, _limitTime, _timeChecks);
                    _skill.SetState(_targetState);
                    return true;
                }
            }
            else
            {
                _skill.currentRating = AttackRating.Miss;
                _skill.SetState(_targetState);
                return true;
            }

            return false;
        }
    }
}
