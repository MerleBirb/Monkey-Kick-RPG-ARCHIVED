//===== PLAYER SKILL =====//
/*
7/12/21
Description:
- Player variation of a Skill

Author: Merlebirb
*/

using UnityEngine;
using System.Collections.Generic;

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
        protected PlayerBattle _actor;
        protected CharacterBattle _target;
        protected List<Transform> _characters;

    	//===== INIT =====//

        public virtual void SettingUpCharacters(CharacterBattle actor, CharacterBattle target)
        {
            if (!_actor) _actor = actor.GetComponent<PlayerBattle>();
            if (!_target) _target = target;
            _characters.Clear();
        }

    	//===== METHODS =====//

        
    }
}
