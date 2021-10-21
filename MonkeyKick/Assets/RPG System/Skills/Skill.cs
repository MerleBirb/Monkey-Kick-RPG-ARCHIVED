// Merle Roji
// 10/21/21

using UnityEngine;
using MonkeyKick.PhysicalObjects.Characters;

namespace MonkeyKick.RPGSystem
{
    public abstract class Skill : ScriptableObject
    {
        #region DESCRIPTORS

        [Header("Description for the Skill.")]
        [SerializeField] protected string skillName;
        [SerializeField] [TextArea(15, 20)] protected string skillDescription;

        [Header("Base Damage / Healing / Value for the skill to use")]
        [SerializeField] protected int skillValue;

        #endregion

        // Damage Calc
        public virtual void Damage(ref int targetHP)
        {
            int finalValue = Mathf.Clamp(skillValue, 1, 99999);    
            bool damageHPBelowZero = (targetHP - finalValue) <= 0;

            // make sure the HP never goes below zero
            if (damageHPBelowZero) targetHP = 0;
            else targetHP -= finalValue;
        }

        // Action
        public abstract void Action(CharacterBattle actor, CharacterBattle target);
    }
}
