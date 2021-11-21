// Merle Roji
// 11/9/21

using UnityEngine;
using System.Collections;
using MonkeyKick.RPGSystem;
using MonkeyKick.CustomPhysics;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class ActorMoveToTarget : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private string _targetState; // the target state that this state will transition to
        private Vector3 _targetPos; // target position to reach
        private float _time; // velocity to reach the target
        private bool _keepMoving = false;
        private bool _stopMoving = false;

        public ActorMoveToTarget(Skill skill, string targetState, Vector3 targetPos, float time, float xOffset = 0f, float yOffset = 0f, float zOffset = 0f)
        {
            _skill = skill;
            _targetState = targetState;
            _targetPos = targetPos;
            _time = time;
            _targetPos += new Vector3(xOffset, yOffset, zOffset);
        }

        public override bool Execute()
        {
            if (!_keepMoving)
            {
                _skill.actor.StartCoroutine(MoveToTarget());
                _keepMoving = true;
            }

            if (_stopMoving)
            {
                _skill.actor.StopCoroutine(MoveToTarget());
                _skill.SetState(_targetState);
                return true;
            }

            return false;
        }

        public IEnumerator MoveToTarget()
        {
            _skill.actorRb.velocity = PhysicsQoL.LinearMove(_skill.actorTransform.position, _targetPos, _time);
            yield return new WaitForSeconds(_time);

            _skill.actorRb.velocity = Vector3.zero;
            _stopMoving = true;
            yield return null;
        }
    }
}
