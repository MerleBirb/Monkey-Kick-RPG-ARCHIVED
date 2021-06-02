
using MonkeyKick.Stats;
using UnityEngine;

namespace MonkeyKick.Battle
{
    [CreateAssetMenu(menuName = "Skills/Damage Skills/Placeholder Damage Skill", fileName = "Placeholder Attack")]
    public class DamageSkill : ScriptableObject, ISkill
    {
        [SerializeField] private string skillName;
        [Multiline, SerializeField] private string skillDescription;
        [SerializeField] private CharacterStatReference damageStat;

        private float speed = 3f;

        public void Action(CharacterBattle _actor, CharacterBattle _target)
        {
            bool _finishedAttack = false;

            var _actorPos = _actor.transform.position;
            var _targetPos = _target.transform.position - new Vector3(-1.2f, 0f, 0f);

            if ((_actorPos != _targetPos) && !_finishedAttack)
            {
                Vector3 pos = Vector3.MoveTowards(_actorPos, _targetPos, speed * Time.deltaTime);
                _actor.rb.MovePosition(pos);

                Damage(_target.Stats.CurrentHP.Stat);
                _finishedAttack = true;
            }

            if (_finishedAttack && (_actorPos != _actor.Stats.battlePos))
            {
                Vector3 pos = Vector3.MoveTowards(_actorPos, _actor.Stats.battlePos, speed * Time.deltaTime);
                _actor.rb.MovePosition(pos);
            }
            else if (_finishedAttack)
            {
                _actor.ChangeBattleState(BattleStates.Wait);
            }
        }

        public string GetName()
        {
            return skillName;
        }

        public string GetDescription()
        {
            return skillDescription;
        }

        public int GetValue()
        {
            return damageStat.Stat.Value;
        }

        private void Damage(CharacterStat _targetHP)
        {
            _targetHP.Value -= damageStat.Stat.Value;
        }
    }
}