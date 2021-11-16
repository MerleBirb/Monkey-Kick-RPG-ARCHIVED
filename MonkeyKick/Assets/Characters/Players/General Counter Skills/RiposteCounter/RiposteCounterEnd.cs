// Merle Roji
// 11/15/21

using MonkeyKick.RPGSystem;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class RiposteCounterEnd : StateAction
    {
        private RiposteCounter _skill; // store the state machine of the skill

        public RiposteCounterEnd(RiposteCounter skill)
        {
            _skill = skill;
        }

        public override bool Execute()
        {
            _skill.counterTimer = 0f;

            return false;
        }
    }
}
