//===== VOLCOMBO =====//
/*
10/3/21 (4th of july)
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
using MonkeyKick.QoL;

namespace MonkeyKick.Skills
{
    [CreateAssetMenu(menuName = "Skills/Player Skills/P-Dawg Skills/Vol-combo", fileName = "Vol-combo")]
    public class VolcomboSkill : PlayerSkill
    {
        //===== VARIABLES =====//
        private enum SequenceState
        {
            WaitingToBegin,
            DashToTarget,
            JabPunch,
            Return,
            EndSequence
        }

        [SerializeField] private SequenceState sequence = SequenceState.WaitingToBegin;

        #region DASH VARIABLES

        [SerializeField] private float dashTime = 1.0f;
        [SerializeField] private float dashOffset = 1.0f;

        #endregion

        #region TIMERS

        // jap punch
        [SerializeField] private float jabRankTimer1;
        [SerializeField] private float jabRankTimer2;
        [SerializeField] private float jabRankTimer3;
        [SerializeField] private float jabRankTimer4;
        [SerializeField] private float jabRankTimer5;

        #endregion

        #region BUTTONS

        [SerializeField] private RectTransform actionButtonPrefab;

        #endregion

        //===== SKILL SEQUENCE =====//
        /*P-Dawg will dash to the opponent, jab punch, cross punch, then upwards
         thrust kick them, then flipping back into place.*/

        public IEnumerator Cor_Action()
        {
            if (sequence == SequenceState.WaitingToBegin) // set up the skill
            {
                _battleSFX = _actor.battleSFX;
                if (!_anim) _anim = _actor.GetAnimator();

                yield return null;

                sequence = SequenceState.DashToTarget;
            }

            if (sequence == SequenceState.DashToTarget) // move torwards the enemy and prepare for jab
            {
                Sound dashSFX = AudioTable.GetSound(SFXNames.DashGeneric001);
                _battleSFX.PhysicalHitTracks[0].PlayRaw(dashSFX.Clip, 1.0f); // play dash sound fx

                _actor.rb.velocity = LinearMove(_actor.transform.position, _target.transform.position, dashTime, dashOffset); // dash to target

                yield return new WaitForSeconds(dashTime - Time.deltaTime);

                _actor.rb.velocity = Vector3.zero; // stop the actor
                AnimQoL.TogglePause(_anim); // pause
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
