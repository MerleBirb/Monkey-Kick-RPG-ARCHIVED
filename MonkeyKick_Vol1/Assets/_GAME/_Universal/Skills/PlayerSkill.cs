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
using MonkeyKick.Battle;

namespace MonkeyKick.Skills
{
    public class PlayerSkill : Skill
    {
    	//===== VARIABLES =====//

        protected enum EffortRanks
        {
            Woops = 0,
            Nice = 1,
            Great = 2,
            Amazing = 3,
            Perfect = 4
        }

        protected EffortRanks _effortRank;
        protected float[] _effortRankValues = { 0.05f, 0.25f, 0.5f, 0.8f, 1.3f };
        protected string[] _effortRankStrings = { "WOOPS...", "NICE!", "GREAT!", "AMAZING!!", "PERFECT!!!" };
        protected PlayerBattle _actor;
        protected CharacterBattle _target;
        protected List<Transform> _characters;

        public StringReference EffortRankText;

    	//===== INIT =====//

        public virtual void SettingUpCharacters(CharacterBattle actor, CharacterBattle target)
        {
            if (!_actor) _actor = actor.GetComponent<PlayerBattle>();
            if (!_target) _target = target;
            _characters = new List<Transform>();
        }

    	//===== METHODS =====//

        protected void TimedButtonPress(float currentTime, float totalTime, Vector3 rankPos, float time1, float time2, float time3, float time4, float time5)
        {
            if (currentTime <= (totalTime * time1)) { SetEffortRank(EffortRanks.Woops, rankPos); return; }
            else if (currentTime <= (totalTime * time2)) { SetEffortRank(EffortRanks.Nice, rankPos); return; }
            else if (currentTime <= (totalTime * time3)) { SetEffortRank(EffortRanks.Great, rankPos); return; }
            else if (currentTime <= (totalTime * time4)) { SetEffortRank(EffortRanks.Amazing, rankPos); return; }
            else if (currentTime <= (totalTime * time5)) { SetEffortRank(EffortRanks.Perfect, rankPos); return; }
            else { SetEffortRank(EffortRanks.Woops, rankPos); _effortValueMultiplier = 0.1f; return; }
        }

        protected void SetEffortRank(EffortRanks newRank, Vector3 pos)
        {
            _effortRank = newRank;
            EffortRankText.Variable.Value = _effortRankStrings[(int)newRank];
            _effortValueMultiplier = _effortRankValues[(int)newRank];

            RectTransform newEffortRank = InstantiateUIPosition(effortRankPrefab, pos);
        }
    }
}
