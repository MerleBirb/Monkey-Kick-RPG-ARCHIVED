
using System.Collections;
using MonkeyKick.Stats;
using UnityEngine;

namespace MonkeyKick.Battle
{
    [CreateAssetMenu(menuName = "Skills/Damage Skills/Placeholder Damage Skill", fileName = "Placeholder Attack")]
    public class DamageSkill : Skill
    {
        [SerializeField] private int damageValue;
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

        public bool IsReady => sequence == SkillState.WaitingToBegin;

        public IEnumerator Cor_Action(CharacterBattle actor, CharacterBattle target)
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

                Damage(target.Stats.CurrentHP);
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

            /*switch(sequence)
            {
                case SkillState.WaitingToBegin:
                {
                    yield return null;

                    sequence = SkillState.FirstSequence;

                    break;
                }
                case SkillState.FirstSequence:
                {
                    actor.rb.velocity = (targetPos - actor.transform.position) / speed;

                    yield return time;

                    actor.rb.velocity = Vector3.zero;
                    sequence = SkillState.SecondSequence;

                    break;
                }
                case SkillState.SecondSequence:
                {
                    yield return null;

                    Damage(target.Stats.CurrentHP);
                    sequence = SkillState.ThirdSequence;

                    break;
                }
                case SkillState.ThirdSequence:
                {
                    actor.rb.velocity = (returnPos - actor.transform.position) / speed;

                    yield return time;

                    actor.rb.velocity = Vector3.zero;
                    sequence = SkillState.EndSequence;

                    break;
                }
                case SkillState.EndSequence:
                {
                    yield return null;

                    actor.ChangeBattleState(BattleStates.Reset);
                    sequence = SkillState.WaitingToBegin;

                    break;
                }
            }*/
        }

        public override void Action(CharacterBattle actor, CharacterBattle target)
        {
            actor.StartCoroutine(Cor_Action(actor, target));

            #region OLD VERSION

            /*switch(sequence)
            {
                case SkillState.WaitingToBegin:
                {
                    if (!actor.finishAction) { sequence = SkillState.FirstSequence; }

                    break;
                }

                case SkillState.FirstSequence:
                {
                    var targetPos = new Vector3(target.transform.position.x + xOffset, target.transform.position.y, target.transform.position.z);
                    bool atTheEnemyLocation = actor.transform.position == targetPos;

                    if(!atTheEnemyLocation) {actor.rb.velocity = new Vector3(speed, actor.rb.velocity.y, actor.rb.velocity.z); }
                    else { sequence = SkillState.SecondSequence; }

                    break;
                }

                case SkillState.SecondSequence:
                {
                    Damage(target.Stats.CurrentHP);
                    sequence = SkillState.ThirdSequence;

                    break;
                }

                case SkillState.ThirdSequence:
                {
                        bool backAtBattlePosition = actor.transform.position.x != actor.Stats.battlePos.x;
                        if (backAtBattlePosition) { actor.rb.velocity = new Vector3(-speed, actor.rb.velocity.y, actor.rb.velocity.z); }
                    else
                    {
                        actor.rb.velocity = Vector3.zero;
                        sequence = SkillState.EndSequence;
                    }

                    break;
                }

                case SkillState.EndSequence:
                {
                    actor.finishAction = true;
                    sequence = SkillState.WaitingToBegin;
                    actor.ChangeBattleState(BattleStates.Reset);

                    break;
                }
            }*/

            #endregion
        }

        public int GetDamage()
        {
            return damageValue;
        }

        private void Damage(CharacterStatReference _targetHP)
        {
            _targetHP.ChangeStat(-damageValue);
        }
    }
}