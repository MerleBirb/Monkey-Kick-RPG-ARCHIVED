//===== PLAYER SKILL =====//
/*
7/12/21
Description:
- Player variation of a Skill

Author: Merlebirb
*/

using UnityEngine;
using System.Collections.Generic;
using MonkeyKick.References;

namespace MonkeyKick.Battle
{
    public class PlayerSkill : Skill
    {
    	//===== VARIABLES =====//

        protected enum EffortRanks
        {
            Cringe = 0,
            Coolio = 1,
            Dope = 2,
            Radical = 3,
            Fantastalicious = 4
        }

        protected EffortRanks _effortRank;
        protected string[] _effortRankStrings = { "CRINGE...", "COOLIO!", "DOPE!", "RADICAL!!", "FANTASTALICIOUS!!!" };
        protected PlayerBattle _actor;
        protected CharacterBattle _target;
        protected List<Transform> _characters;

        public StringReference EffortRankText;

    	//===== INIT =====//

        public virtual void SettingUpCharacters(CharacterBattle actor, CharacterBattle target)
        {
            if (!_actor) _actor = actor.GetComponent<PlayerBattle>();
            if (!_target) _target = target;
            _characters.Clear();
        }

    	//===== METHODS =====//

        protected void SetEffortRank(EffortRanks newRank)
        {
            _effortRank = newRank;
            EffortRankText.Variable.Value = _effortRankStrings[(int)newRank];
        }
    }
}
