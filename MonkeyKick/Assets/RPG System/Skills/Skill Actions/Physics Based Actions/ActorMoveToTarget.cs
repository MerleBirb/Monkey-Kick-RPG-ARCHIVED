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
        private bool _keepMoving = true;

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
            if (_keepMoving) _skill.actorRb.velocity = _velocity; // set the velocity of the actor

            if (_xOffset != 0f)
            {
                if ((_skill.actorTransform.position - _targetPos).sqrMagnitude < _xOffset)
                {
                    _keepMoving = false;
                    _skill.actorRb.velocity = Vector3.zero;
                    _skill.SetState(_targetState); // change state when ready
                    return true;
                }
            }
            else
            {
                if (_skill.actorTransform.position == _targetPos)
                {
                    _keepMoving = false;
                    _skill.actorRb.velocity = Vector3.zero;
                    _skill.SetState(_targetState); // change state when ready
                    return true;
                }
            }

            return false;
        }
    }
}
