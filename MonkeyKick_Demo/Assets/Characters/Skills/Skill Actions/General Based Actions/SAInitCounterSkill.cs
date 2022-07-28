// Merle Roji 7/28/22

using MonkeyKick.Characters;

namespace MonkeyKick.Skill
{
    public class SAInitCounterSkill : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private Skill[] _possibleCounters;

        public SAInitCounterSkill(Skill skill, Skill[] possibleCounters)
        {
            _skill = skill;
            _possibleCounters = possibleCounters;
        }

        public override bool Execute()
        {
            if (_possibleCounters != null)
            {
                for (int i = 0; i < _possibleCounters.Length; ++i)
                {
                    _possibleCounters[i].Init(_skill.Target, new CharacterBattle[] { _skill.Actor });
                    if (i == _possibleCounters.Length) return true;
                }
            }

            return false;
        }
    }
}

