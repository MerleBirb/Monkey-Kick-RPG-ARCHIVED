// Merle Roji
// 11/10/21

using System;
using UnityEngine;
using MonkeyKick.RPGSystem;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class DelayState : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private string _targetState; // the target state that this state will transition to
        private float _time; // time that the state will be delayed by
        private float _currentTime; // timer

        public DelayState(Skill skill, string targetState, float delayTime = 0f)
        {
            _skill = skill;
            _targetState = targetState;
            _time = delayTime;
            _currentTime = 0f;
        }

        public override bool Execute()
        {   
            if (_currentTime < _time)
            {
                _currentTime += Time.deltaTime;
                return false;
            }
            else
            {
                _skill.SetState(_targetState);
                return true;  
            }
        }
    }
}
