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

                yield return null;

                sequence = SequenceState.JumpingToTarget;
            }
            if (sequence == SequenceState.JumpingToTarget)
            {
                ParabolaJump();

                float time = CalculateParabolaData(_actor, _target, jumpHeight, newGravity).timeToTarget;
                yield return new WaitForSeconds(time);

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
            Physics.gravity = Vector3.up * newGravity;

            _actor.rb.velocity = CalculateParabolaData(_actor, _target, jumpHeight, newGravity).initialVelocity;
            _target.rb.mass = 10000;
        }
    }
}
