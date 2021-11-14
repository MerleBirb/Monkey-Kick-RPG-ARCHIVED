// Merle Roji
// 11/13/21

using MonkeyKick.RPGSystem;
using MonkeyKick.PhysicalObjects.Characters;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class InitCounterSkill : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private CounterSkill[] _possibleCounters;

        public InitCounterSkill(Skill skill, CounterSkill[] possibleCounters)
        {
            _skill = skill;
            _possibleCounters = possibleCounters;
        }

        public override bool Execute()
        {
            if (_possibleCounters != null)
            {
                for (int i = 0; i < _possibleCounters.Length; i++)
                {
                    _possibleCounters[i].Init((PlayerBattle)_skill.target, (EnemyBattle)_skill.actor);

                    if (i < _possibleCounters.Length - 1) return false;
                    else return true;
                }
            }

            return false;
        }
    }
}
