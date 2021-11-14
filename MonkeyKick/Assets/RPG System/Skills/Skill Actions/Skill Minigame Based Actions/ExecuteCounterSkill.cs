// Merle Roji
// 11/13/21

using MonkeyKick.RPGSystem;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class ExecuteCounterSkill : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private CounterSkill[] _possibleCounters;

        public ExecuteCounterSkill(Skill skill, CounterSkill[] possibleCounters)
        {
            _skill = skill;
            _possibleCounters = possibleCounters;
        }

        public override bool Execute()
        {
            if (_possibleCounters != null)
            {
                foreach (CounterSkill c in _possibleCounters)
                {
                    c.Execute();
                }
            }

            return false;
        }
    }
}
