// Merle Roji 7/28/22

using MonkeyKick.Characters;

namespace MonkeyKick.Skill
{
    public class SAEndSkill : StateAction
    {
        private Skill _skill; // store the state machine of the skill

        public SAEndSkill(Skill skill)
        {
            _skill = skill;
        }

        public override bool Execute()
        {
            _skill.Actor.ResetAfterAction();
            _skill.Target.BattleState = BattleStates.Wait;

            return true;
        }
    }
}
