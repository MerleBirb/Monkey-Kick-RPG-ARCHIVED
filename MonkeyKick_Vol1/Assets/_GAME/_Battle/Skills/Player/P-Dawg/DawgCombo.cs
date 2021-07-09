//===== DAWG COMBO =====//
/*
7/4/21 (4th of july)
Description:
- P-Dawg's default combination move

Author: Merlebirb
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.CameraTools;

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
        private List<Transform> _characters;

        [SerializeField] private SequenceState sequence = SequenceState.WaitingToBegin;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float returnSeconds = 1;
        [SerializeField] private float newGravity = -30f;
        [SerializeField] private Vector3 camOffset;

    	//===== METHODS =====//

        private IEnumerator Cor_Action(CharacterBattle actor, CharacterBattle target)
        {
            if (sequence == SequenceState.WaitingToBegin)
            {
                // store private vars
                if (!_actor) _actor = actor;
                if (!_target) _target = target;
                if (!_mainCam) _mainCam = Camera.main;
                _characters.Clear();

                yield return null;

                _characters.Add(_actor.transform);
                _characters.Add(_target.transform);

                sequence = SequenceState.JumpingToTarget;
            }
            if (sequence == SequenceState.JumpingToTarget)
            {
                float time = CalculateParabolaData(_actor, _target, jumpHeight, newGravity).timeToTarget;

                ParabolaJump(
                        newGravity,
                        CalculateParabolaData(_actor, _target, jumpHeight, newGravity),
                        _actor,
                        _target);

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
    }
}
