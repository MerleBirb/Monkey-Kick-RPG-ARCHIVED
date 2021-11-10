// Merle Roji
// 11/9/21

using System;
using UnityEngine;
using MonkeyKick.RPGSystem;
using MonkeyKick.LogicPatterns.StateMachines;

namespace MonkeyKick
{
    public class ActorMoveToTarget : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private string _targetState; // the target state that this state will transition to
        private float _time; // time it takes to reach the target
        private float _xOffset; // x offset from the target

        public ActorMoveToTarget(Skill skill, string targetState, float time = 1f, float xOffset = 0f)
        {
            _skill = skill;
            _targetState = targetState;
            _time = time;
            _xOffset = xOffset;
        }

        public override bool Execute()
        {
            Vector3 actorPos = _skill.actorTransform.position; // store the actor's position
            Vector3 targetPos = _skill.targetTransform.position; // store the target's position

            Vector3 goalPosition = new Vector3(targetPos.x + _xOffset, targetPos.y, targetPos.z); // where to move to
            Vector3 goalVelocity = (goalPosition - actorPos) / _time; // velocity equation to reach the goalPosition
            _skill.actorRb.velocity = goalVelocity; // set the velocity of the actor

            if ((float)Math.Round(actorPos.x) == (float)Math.Round(goalPosition.x))
            {
                _skill.SetState(_targetState); // change state when ready
                return true;
            }

            return false;
        }
    }
}
