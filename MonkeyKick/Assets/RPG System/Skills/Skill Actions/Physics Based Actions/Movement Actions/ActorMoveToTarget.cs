// Merle Roji
// 11/9/21

using System;
using UnityEngine;
using MonkeyKick.RPGSystem;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class ActorMoveToTarget : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private string _targetState; // the target state that this state will transition to
        private Vector3 _targetPos; // target position to reach
        private Vector3 _velocity; // velocity to reach the target
        private float _xOffset; // x offset from the target

        public ActorMoveToTarget(Skill skill, string targetState, Vector3 targetPos, Vector3 velocity, float xOffset = 0f)
        {
            _skill = skill;
            _targetState = targetState;
            _targetPos = targetPos;
            _velocity = velocity;
            _xOffset = xOffset;
        }

        public override bool Execute()
        {
            _skill.actorRb.velocity = _velocity; // set the velocity of the actor

            if ((float)Math.Round(_skill.actorTransform.position.x) == (float)Math.Round(_targetPos.x + _xOffset))
            {
                _skill.actorRb.velocity = Vector3.zero;
                _skill.SetState(_targetState); // change state when ready
                return true;
            }

            return false;
        }
    }
}
