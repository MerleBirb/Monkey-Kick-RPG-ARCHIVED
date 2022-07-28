// Merle Roji 7/28/22

using MonkeyKick.Characters;

namespace MonkeyKick.Skill
{
    public class SASetPlayerStateFromEnemy : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private BattleStates _newState; // store the state the player will change into

        public SASetPlayerStateFromEnemy(Skill skill, BattleStates newState)
        {
            _skill = skill;
            _newState = newState;
        }

        public override bool Execute()
        {
            if (_skill.Target.BattleState != _newState)
            {
                _skill.Target.BattleState = _newState;
                return true;
            }

            return false;
        }
    }
}

