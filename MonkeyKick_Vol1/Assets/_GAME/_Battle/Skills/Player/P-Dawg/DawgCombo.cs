//===== DAWG COMBO =====//
/*
7/4/21 (4th of july)
Description:
- P-Dawg's default combination move

Author: Merlebirb
*/

using System.Collections;
using UnityEngine;
using Unity.Collections;

namespace MonkeyKick.Battle
{
    [CreateAssetMenu(menuName = "Skills/Damage Skills/Dawg Combo", fileName = "Dawg Combo")]
    public class DawgCombo : Skill
    {
    	//===== VARIABLES =====//

        private enum SequenceState
        {
            WaitingToBegin,
            JumpingToTarget,
            Return,
            EndSequence
        }

        private CharacterBattle _target;
        private CharacterBattle _actor;
        private float _seconds;

        [SerializeField] private SequenceState sequence = SequenceState.WaitingToBegin;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float returnSeconds = 1;
        [SerializeField] private float newGravity = -30f;

    	//===== METHODS =====//

        private IEnumerator Cor_Action(CharacterBattle actor, CharacterBattle target)
        {
            if (sequence == SequenceState.WaitingToBegin)
            {
                // store the actor and target into private variables
                if(!_actor) _actor = actor;
                if(!_target) _target = target;

                FreeUpPhysics(_actor); // turn off gravity by default

                yield return null;

                sequence = SequenceState.JumpingToTarget;
            }
            if (sequence == SequenceState.JumpingToTarget)
            {
                ParabolaJump();

                yield return new WaitForSeconds(CalculateJumpData().timeToTarget);

                _actor.rb.velocity = Vector3.zero;
                Damage(_target);

                sequence = SequenceState.Return;
            }
            if (sequence == SequenceState.Return)
            {
                Vector3 returnPos = new Vector3(_actor.Stats.battlePos.x, _actor.transform.position.y, _actor.Stats.battlePos.z);
                _actor.rb.velocity = (returnPos - actor.transform.position) / returnSeconds;

                yield return new WaitForSeconds(returnSeconds - Time.fixedDeltaTime);

                _actor.rb.velocity = Vector3.zero;

                sequence = SequenceState.EndSequence;
            }
            if (sequence == SequenceState.EndSequence)
            {
                yield return null;

                _actor.ChangeBattleState(BattleStates.Reset);
                sequence = SequenceState.WaitingToBegin;
            }
        }

        public override void Action(CharacterBattle actor, CharacterBattle target)
        {
            actor.StartCoroutine(Cor_Action(actor, target));
        }

        private void ParabolaJump()
        {
            ResetPhysics(_actor); // turn gravity back on for the jump
            Physics.gravity = Vector3.up * newGravity;

            _actor.rb.velocity = CalculateJumpData().initialVelocity;
            _target.rb.mass = 10000;
        }

        private JumpData CalculateJumpData() // parabola for jump
        {
            Vector3 targetPos = _target.transform.position;
            Vector3 actorPos = _actor.transform.position;

            float displacementY = targetPos.y - actorPos.y;
            Vector3 displacementXZ = new Vector3 (targetPos.x - actorPos.x, 0, targetPos.z - actorPos.z);

            float time = Mathf.Sqrt((-2 * jumpHeight) / newGravity) + Mathf.Sqrt(2 *(displacementY - jumpHeight) / newGravity);
            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * newGravity * jumpHeight);
            Vector3 velocityXZ = displacementXZ / time;

            return new JumpData(velocityXZ + velocityY * -Mathf.Sign(newGravity), time);
        }

        private struct JumpData
        {
            public readonly Vector3 initialVelocity;
            public readonly float timeToTarget;

            public JumpData(Vector3 initialVelocity, float timeToTarget)
            {
                this.initialVelocity = initialVelocity;
                this.timeToTarget = timeToTarget;
            }
        }

        private void DrawPath()
        {
            JumpData jumpData = CalculateJumpData();
            Vector3 previousDrawPoint = _actor.transform.position;

            int resolution = 30;
            for (int i = 1; i <= resolution; i++)
            {
                float simulationTime = i / (float)resolution * jumpData.timeToTarget;
                Vector3 displacement = jumpData.initialVelocity * simulationTime + Vector3.up * newGravity * simulationTime * simulationTime / 2f;
                Vector3 drawPoint = _actor.transform.position + displacement;
                Debug.DrawLine(previousDrawPoint, -drawPoint, Color.green);
                previousDrawPoint = drawPoint;
            }
        }
    }
}
