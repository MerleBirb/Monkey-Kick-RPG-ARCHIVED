// Merle Roji
// 11/15/21

using MonkeyKick.RPGSystem;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class InterruptState : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private string _targetState; // the target state that this state will transition to

        public InterruptState(Skill skill, string targetState)
        {
            _skill = skill;
            _targetState = targetState;
        }
        
        public override bool Execute()
        {
            if (_skill.actor.isInterrupted)
            {
                _skill.actor.CharacterPhysics.ResetMovement();
                _skill.SetState(_targetState);
                _skill.actor.isInterrupted = false;

                return true;
            }

            return false;
        }
    }
}
