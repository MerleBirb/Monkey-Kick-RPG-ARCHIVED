// Merle Roji 7/28/22


namespace MonkeyKick.Skills
{
    public class SAInterruptAction : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private string _targetState; // the target state that this state will transition to

        public SAInterruptAction(Skill skill, string targetState)
        {
            _skill = skill;
            _targetState = targetState;
        }

        public override bool Execute()
        {
            if (_skill.Actor.IsInterrupted)
            {
                _skill.Actor.ResetMovement();
                _skill.SetState(_targetState);
                _skill.Actor.IsInterrupted = false;

                return true;
            }

            return false;
        }
    }
}

