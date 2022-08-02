// Merle Roji 8/1/22

using MonkeyKick.Skills;

namespace MonkeyKick.Skills
{
    public class SARiposteEnd : StateAction
    {
        private SKRiposteCounter _skill; // store the state machine of the skill

        public SARiposteEnd(SKRiposteCounter skill)
        {
            _skill = skill;
        }

        public override bool Execute()
        {
            _skill.CounterTimer = 0f;

            return false;
        }
    }
}
