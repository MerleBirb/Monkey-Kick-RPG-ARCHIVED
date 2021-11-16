// Merle Roji
// 11/13/21

using MonkeyKick.RPGSystem;
using MonkeyKick.Characters;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class InitCounterSkill : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private Skill[] _possibleCounters;

        public InitCounterSkill(Skill skill, Skill[] possibleCounters)
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
                    _possibleCounters[i].Init(_skill.target, new CharacterBattle[] { _skill.actor });
                    if (i == _possibleCounters.Length) return true;
                }
            }

            return false;
        }
    }
}
