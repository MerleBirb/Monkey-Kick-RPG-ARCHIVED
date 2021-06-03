
using MonkeyKick.Stats;
using UnityEngine;

namespace MonkeyKick.Battle
{
    [CreateAssetMenu(menuName = "Skills/Damage Skills/Placeholder Damage Skill", fileName = "Placeholder Attack")]
    public class DamageSkill : Skill
    {
        [SerializeField] private int damageValue;
        [SerializeField] private float speed;
        [SerializeField] private float xOffset;

        private enum SkillState
        {
            WaitingToBegin,
            BeginSequence,
            ActionSequence,
            ReturnFromSequence,
            EndSequence
        }

        [SerializeField] private SkillState sequence = SkillState.WaitingToBegin;

        public override void Action(CharacterBattle _actor, CharacterBattle _target)
        {
            switch(sequence)
            {
                case SkillState.WaitingToBegin:
                {
                    if (!_actor.finishAction) { sequence = SkillState.BeginSequence; }

                    break;
                }

                case SkillState.BeginSequence:
                {
                    var _targetPos = new Vector3(_target.transform.position.x + xOffset, _target.transform.position.y, _target.transform.position.z);
                    bool _atTheEnemyLocation = _actor.transform.position != _targetPos;

                    if(_atTheEnemyLocation) {_actor.rb.velocity = new Vector3(speed, _actor.rb.velocity.y, _actor.rb.velocity.z); }
                    else { sequence = SkillState.ActionSequence; }

                    break;
                }

                case SkillState.ActionSequence:
                {
                    Damage(_target.Stats.CurrentHP);
                    sequence = SkillState.ReturnFromSequence;

                    break;
                }

                case SkillState.ReturnFromSequence:
                {
                        bool _backAtBattlePosition = _actor.transform.position.x != _actor.Stats.battlePos.x;
                        if (_backAtBattlePosition) { _actor.rb.velocity = new Vector3(-speed, _actor.rb.velocity.y, _actor.rb.velocity.z); }
                    else
                    {
                        _actor.rb.velocity = Vector3.zero;
                        sequence = SkillState.EndSequence;
                    }

                    break;
                }

                case SkillState.EndSequence:
                {
                    _actor.finishAction = true;
                    sequence = SkillState.BeginSequence;
                    _actor.ChangeBattleState(BattleStates.Reset);

                    break;
                }
            }
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