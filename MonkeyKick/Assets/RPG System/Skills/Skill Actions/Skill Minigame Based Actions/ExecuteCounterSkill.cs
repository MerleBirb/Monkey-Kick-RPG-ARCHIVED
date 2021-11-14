// Merle Roji
// 11/13/21

using MonkeyKick.RPGSystem;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class ExecuteCounterSkill : StateAction
    {
        private Skill[] _possibleCounters; // list of all possible counters
        private bool _fixedUpdate; // if this is on a fixed update action or not

        public ExecuteCounterSkill(Skill[] possibleCounters, bool fixedUpdate)
        {
            _possibleCounters = possibleCounters;
            _fixedUpdate = fixedUpdate;
        }

        public override bool Execute()
        {
            if (_possibleCounters != null)
            {
                if (_fixedUpdate)
                {
                    foreach (Skill c in _possibleCounters)
                    {
                        c.FixedTick();
                    }
                }
                else
                {
                    foreach (Skill c in _possibleCounters)
                    {
                        c.Tick();
                    }
                }
            }

            return false;
        }
    }
}
