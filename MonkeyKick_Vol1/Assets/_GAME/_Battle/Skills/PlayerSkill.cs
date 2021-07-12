//===== PLAYER SKILL =====//
/*
7/12/21
Description:
- Player variation of a Skill

Author: Merlebirb
*/

namespace MonkeyKick.Battle
{
    public class PlayerSkill : Skill
    {
    	//===== VARIABLES =====//

        protected enum EffortRanks
        {
            Doozy,
            Groovy,
            Swag,
            Awesome,
            Fantastalicious
        }

        protected EffortRanks _effortRank;
        protected string[] _effortRankStrings = { "DOOZY...", "GROOVY!", "SWAG!", "AWESOME!!", "FANTASTALICIOUS!!!" };
        protected SkillControls _skillControls;

    	//===== INIT =====//

        public void SetControls(SkillControls controls)
        {
            if (_skillControls == controls) return;
            _skillControls = controls;
        }

    	//===== METHODS =====//
    }
}
