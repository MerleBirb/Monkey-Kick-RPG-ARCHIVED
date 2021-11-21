// Merle Roji
// 11/9/21

using MonkeyKick.RPGSystem;
using MonkeyKick.RPGSystem.Characters;

namespace MonkeyKick.LogicPatterns.StateMachines
{
    public class EndSkill : StateAction
    {
        private Skill _skill; // store the state machine of the skill

        public EndSkill(Skill skill)
        {
            _skill = skill;
        }

        public override bool Execute()
        {
            _skill.actor.ResetAfterAction();
            _skill.target.BattleState = BattleStates.Wait;

            return true;
        }
    }
}
