// Merle Roji
// 11/10/21

using MonkeyKick.RPGSystem;
using MonkeyKick.PhysicalObjects.Characters;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class SetPlayerToStateFromEnemy : StateAction
    {
        private Skill _skill; // store the state machine of the skill
        private BattleStates _newState; // store the state the player will change into

        public SetPlayerToStateFromEnemy(Skill skill, BattleStates newState)
        {
            _skill = skill;
            _newState = newState;
        }

        public override bool Execute()
        {
            _skill.target.BattleState = _newState;
            return true;
        }
    }
}
