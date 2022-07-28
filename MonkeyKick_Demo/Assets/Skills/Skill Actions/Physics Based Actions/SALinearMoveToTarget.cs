// Merle Roji 7/28/22

using UnityEngine;
using System.Collections;

namespace MonkeyKick.Skills
{
    public class SALinearMoveToTarget : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private string _targetState; // the target state that this state will transition to
        private Vector3 _targetPos; // target position to reach
        private float _time; // velocity to reach the target
        private bool _keepMoving = false;
        private bool _stopMoving = false;

        public SALinearMoveToTarget(Skill skill, string targetState, Vector3 targetPos, float time, float xOffset = 0f, float yOffset = 0f, float zOffset = 0f)
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
                _skill.Actor.StartCoroutine(MoveToTarget());
                _keepMoving = true;
            }

            if (_stopMoving)
            {
                _skill.Actor.StopCoroutine(MoveToTarget());
                _skill.SetState(_targetState);
                return true;
            }

            if (_skill.Actor.IsInterrupted)
            {

            }

            return false;
        }

        public IEnumerator MoveToTarget()
        {
            _skill.ActorRb.velocity = PhysicsQoL.LinearMove(_skill.ActorTransform.position, _targetPos, _time);
            yield return new WaitForSeconds(_time);

            _skill.ActorRb.velocity = Vector3.zero;
            _stopMoving = true;
            yield return null;
        }
    }
}

