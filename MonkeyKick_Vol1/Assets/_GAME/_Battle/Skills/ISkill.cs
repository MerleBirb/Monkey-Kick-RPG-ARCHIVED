//===== INTERFACE SKILL =====//
/*
5/30/21
Description:
- A Skill interface that holds all rules for all skills

Author: Merlebirb
*/

using UnityEngine;

namespace MonkeyKick.Battle
{
    public interface ISkill
    {
        void Action(CharacterBattle _actor, CharacterBattle _target); // what the skill actually does

        string GetName();
        string GetDescription();

        int GetValue(); // returns the stat value the skill uses
    }
}
