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
using MonkeyKick.Battle;
using MonkeyKick.AudioFX;
using MonkeyKick.UI;

namespace MonkeyKick.Skills
{
    [CreateAssetMenu(menuName = "Skills/Damage Skills/Dawg Combo", fileName = "Dawg Combo")]
    public class DawgCombo : PlayerSkill
    {
    	//===== VARIABLES =====//

        private enum SequenceState
        {
            WaitingToBegin,
            JumpingToTarget,
            Return,
            EndSequence
        }

        [SerializeField] private SequenceState sequence = SequenceState.WaitingToBegin;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float newGravity = -30f; 

        #region TIMERS

        [SerializeField] private float jumpRankTimer1;
        [SerializeField] private float jumpRankTimer2;
        [SerializeField] private float jumpRankTimer3;
        [SerializeField] private float jumpRankTimer4;
        [SerializeField] private float jumpRankTimer5;

        #endregion

        #region BUTTONS

        [SerializeField] private RectTransform actionButtonPrefab;

        #endregion

    	//===== METHODS =====//

        public IEnumerator Cor_Action()
        {
            if (sequence == SequenceState.WaitingToBegin)
            {
                _battleSFX = _actor.battleSFX;

                yield return null;

                sequence = SequenceState.JumpingToTarget;
            }
            if (sequence == SequenceState.JumpingToTarget)
            {
                ParabolaData jumpData = CalculateParabolaData(_actor.transform.position, _target.transform.position,
                                        jumpHeight, _target.Stats.Height, newGravity);
                float totalTime = jumpData.timeToTarget;
                float currentTime = 0f;

                Sound jumpSFX = AudioTable.GetSound(SFXNames.DashGeneric001);
                Sound attackSFX = AudioTable.GetSound(SFXNames.XtraLargeHit001);

                IDisplayUI xButton = InstantiateUI(
                    actionButtonPrefab,
                    new Vector3(_target.transform.position.x - _target.Stats.Height, _target.transform.position.y - _target.Stats.Height, _target.transform.position.z)
                );

                ParabolaJump(
                        newGravity,
                        jumpData,
                        _actor,
                        _target);

                _battleSFX.PhysicalHitTracks[0].PlayRaw(
                    jumpSFX.Clip,
                    1f
                );

                _effortValueMultiplier = _effortRankValues[(int)EffortRanks.Woops];

                while(currentTime < totalTime)
                {
                    currentTime += Time.deltaTime;

                    if (_actor.southPressed)
                    {
                        Destroy(xButton.GetGameObject());
                        Vector3 rankPos = new Vector3(_target.transform.position.x - _target.Stats.Height, _target.transform.position.y - _target.Stats.Height, _target.transform.position.z);
                        TimedButtonPress(currentTime, totalTime, rankPos, jumpRankTimer1, jumpRankTimer2, jumpRankTimer3, jumpRankTimer4, jumpRankTimer5);
                    }

                    yield return null;
                }

                yield return null;

                _battleSFX.PhysicalHitTracks[0].PlayRaw(
                    attackSFX.Clip,
                    0.3f
                );
                _actor.rb.velocity = Vector3.zero;
                Damage(_target);

                sequence = SequenceState.Return;
            }
            if (sequence == SequenceState.Return)
            {
                ParabolaData jumpData = CalculateParabolaData(_actor.transform.position, _actor.Stats.battlePos,
                                        jumpHeight, _actor.Stats.Height - 1f, newGravity);
                float totalTime = jumpData.timeToTarget;

                ParabolaJump(
                        newGravity,
                        jumpData,
                        _actor,
                        _target);

                yield return new WaitForSeconds(totalTime);

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
            SettingUpCharacters(actor, target);
            if (!_mainCam) _mainCam = Camera.main;

            actor.StartCoroutine(Cor_Action());
        }
    }
}
