
using System.Collections;
using UnityEngine;

namespace MonkeyKick.Battle
{
    [CreateAssetMenu(menuName = "Skills/Damage Skills/Placeholder Damage Skill", fileName = "Placeholder Attack")]
    public class DamageSkill : Skill
    {
        [SerializeField] private float seconds;
        [SerializeField] private float xOffset;

        private enum SkillState
        {
            WaitingToBegin,
            FirstSequence,
            SecondSequence,
            ThirdSequence,
            EndSequence
        }

        [SerializeField] private SkillState sequence = SkillState.WaitingToBegin;

        private IEnumerator Cor_Action(CharacterBattle actor, CharacterBattle target)
        {
            Vector3 targetPos = new Vector3(target.transform.position.x + xOffset, actor.transform.position.y, target.transform.position.z);
            Vector3 returnPos = new Vector3(actor.Stats.battlePos.x, actor.transform.position.y, actor.Stats.battlePos.z);
            WaitForSeconds time = new WaitForSeconds(seconds - Time.fixedDeltaTime);

            if (sequence == SkillState.WaitingToBegin)
            {
                yield return null;

                sequence = SkillState.FirstSequence;
            }
            if (sequence == SkillState.FirstSequence)
            {
                actor.rb.velocity = (targetPos - actor.transform.position) / seconds;

                yield return new WaitForSeconds(seconds - Time.fixedDeltaTime);

                actor.rb.velocity = Vector3.zero;
                sequence = SkillState.SecondSequence;
            }
            if (sequence == SkillState.SecondSequence)
            {
                yield return null;

                Damage(target);
                sequence = SkillState.ThirdSequence;
            }
            if (sequence == SkillState.ThirdSequence)
            {
                actor.rb.velocity = (returnPos - actor.transform.position) / seconds;

                yield return new WaitForSeconds(seconds - Time.fixedDeltaTime);

                actor.rb.velocity = Vector3.zero;
                sequence = SkillState.EndSequence;
            }
            if (sequence == SkillState.EndSequence)
            {
                yield return null;

                actor.ChangeBattleState(BattleStates.Reset);
                sequence = SkillState.WaitingToBegin;
            }
        }

        public override void Action(CharacterBattle actor, CharacterBattle target)
        {
            actor.StartCoroutine(Cor_Action(actor, target));
        }
    }
}