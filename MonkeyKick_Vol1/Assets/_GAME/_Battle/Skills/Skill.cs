// ===== SKILL =====//
/*
6/2/21
Description:
- Abstract class for skills used in battle.

Author: Merlebirb
*/

using MonkeyKick.Stats;
using UnityEngine;

namespace MonkeyKick.Battle
{
    public abstract class Skill : ScriptableObject
    {
        public string skillName;
        [Multiline] public string skillDescription;

        public virtual void Action(CharacterBattle _actor, CharacterBattle _target)
        {
            return;
        }

        public string GetName()
        {
            return skillName;
        }

        public string GetDescription()
        {
            return skillDescription;
        }
    }
}
