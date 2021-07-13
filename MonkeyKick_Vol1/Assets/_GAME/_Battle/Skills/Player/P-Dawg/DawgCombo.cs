//===== DAWG COMBO =====//
/*
7/4/21 (4th of july)
Description:
- P-Dawg's default combination move

Author: Merlebirb
*/

using System.Collections;
using UnityEngine;

namespace MonkeyKick.Battle
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
        [SerializeField] private float returnSeconds = 1;
        [SerializeField] private float newGravity = -30f;
        //[SerializeField] private Vector3 camOffset;

    	//===== METHODS =====//

        public IEnumerator Cor_Action(CharacterBattle actor, CharacterBattle target)
        {
            if (sequence == SequenceState.WaitingToBegin)
            {
                SettingUpCharacters(actor, target);

                if (!_mainCam) _mainCam = Camera.main;

                yield return null;

                _characters.Add(_actor.transform);
                _characters.Add(_target.transform);

                sequence = SequenceState.JumpingToTarget;
            }
            if (sequence == SequenceState.JumpingToTarget)
            {
                float totalTime = CalculateParabolaData(_actor, _target, jumpHeight, newGravity).timeToTarget;
                float currentTime = 0f;

                ParabolaJump(
                        newGravity,
                        CalculateParabolaData(_actor, _target, jumpHeight, newGravity),
                        _actor,
                        _target);


                while(currentTime < totalTime)
                {
                    currentTime += Time.deltaTime;

                    if (_actor.southPressed)
                    {
                        if (currentTime <= (totalTime * 0.5f)) { SetEffortRank(EffortRanks.Cringe); _effortValueMultiplier = 0.1f; }
                        else if (currentTime <= (totalTime * 0.6f)) { SetEffortRank(EffortRanks.Coolio); _effortValueMultiplier = 0.4f; }
                        else if (currentTime <= (totalTime * 0.7f)) { SetEffortRank(EffortRanks.Dope); _effortValueMultiplier = 0.75f; }
                        else if (currentTime <= (totalTime * 0.8f)) { SetEffortRank(EffortRanks.Radical); _effortValueMultiplier = 1f; }
                        else if (currentTime <= (totalTime * 0.9f)) { SetEffortRank(EffortRanks.Fantastalicious); _effortValueMultiplier = 1.2f; }
                        else { SetEffortRank(EffortRanks.Cringe); }

                        Debug.Log("Button Pressed");
                    }

                    yield return null;
                }

                yield return null;

                _actor.rb.velocity = Vector3.zero;
                Damage(_target);

                sequence = SequenceState.Return;
            }
            if (sequence == SequenceState.Return)
            {
                _actor.rb.velocity = LinearReturn(_actor.Stats.battlePos, _actor.transform.position, returnSeconds);

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
