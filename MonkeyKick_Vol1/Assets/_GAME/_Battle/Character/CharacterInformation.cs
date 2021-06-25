//===== CHARACTER INFORMATION =====//
/*
5/22/21
Description:
- Holds character information, abstract class

Author: Merlebirb
*/

using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.Stats;

namespace MonkeyKick.Battle
{
    public class CharacterInformation : ScriptableObject
    {
        protected int statClamp = 9999;

        public string CharacterName = "New Character";
        [Multiline] public string Description;
        public int Level;

        public CharacterStatReference MaxHP;
        public CharacterStatReference CurrentHP;

        public CharacterStatReference MaxKP; // energy / mana system
        public CharacterStatReference CurrentKP;

        public CharacterStatReference Muscle; // physical strength
        public CharacterStatReference Toughness; // physical defense
        public CharacterStatReference Smarts; // energy attack
        public CharacterStatReference Tenacity; // energy defense
        public CharacterStatReference Speed; // speed... what else?
        public CharacterStatReference Swag; // luck / crit chance

        public bool isAlive;

        public Vector3 battlePos; // neutral position during battle

        public List<Skill> skillList;

        public virtual void OnValidate()
        {
            Level = Mathf.Clamp(Level, 1, 100);

            MaxHP.Stat.BaseValue = Mathf.Clamp(MaxHP.Stat.BaseValue, 1, statClamp);
            CurrentHP.Stat.BaseValue = Mathf.Clamp(CurrentHP.Stat.BaseValue, 0, MaxHP.Stat.BaseValue);

            MaxKP.Stat.BaseValue = Mathf.Clamp(MaxKP.Stat.BaseValue, 1, statClamp);
            CurrentKP.Stat.BaseValue = Mathf.Clamp(CurrentKP.Stat.BaseValue, 0, MaxKP.Stat.BaseValue);

            Muscle.Stat.BaseValue = Mathf.Clamp(Muscle.Stat.BaseValue, 1, statClamp);
            Toughness.Stat.BaseValue = Mathf.Clamp(Toughness.Stat.BaseValue, 1, statClamp);
            Smarts.Stat.BaseValue = Mathf.Clamp(Smarts.Stat.BaseValue, 1, statClamp);
            Tenacity.Stat.BaseValue = Mathf.Clamp(Tenacity.Stat.BaseValue, 1, statClamp);
            Speed.Stat.BaseValue = Mathf.Clamp(Speed.Stat.BaseValue, 1, statClamp);
            Swag.Stat.BaseValue = Mathf.Clamp(Swag.Stat.BaseValue, 1, statClamp);
        }
    }
}