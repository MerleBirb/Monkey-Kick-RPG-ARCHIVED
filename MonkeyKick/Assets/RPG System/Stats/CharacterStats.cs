// Merle Roji
// 10/11/21

using UnityEngine;
using System.Collections.Generic;

namespace MonkeyKick.RPGSystem
{
    [CreateAssetMenu(fileName = "Stats", menuName = "RPGSystem/Stats/CharacterStats", order = 1)]
    public class CharacterStats : ScriptableObject
    {
        [Header("Basic Information")]
        public string CharacterName = "Name";
        public uint Level = 1;
        [TextArea(15, 20)]
        public string Description = "Description";

        [Header("How much damage the character can take")]
        public uint MaxHP;
        public uint CurrentHP;

        [Header("How much Ki the character has")]
        public uint MaxKi;
        public uint CurrentKi;

        [Header("Physical Damage")]
        public uint Muscle;

        [Header("Physical Defense and Status Damage")]
        public uint Toughness;

        [Header("Ki Damage scaling")]
        public uint Smarts;

        [Header("Ki Defense and Status Chance")]
        public uint Willpower;

        [Header("Affects where character goes in Turn Order")]
        public uint Speed;

        [Header("Critical Chance and Shop Price")]
        public uint Swag;

        [Header("List of Skills the player can use.")]
        public List<Skill> SkillList;
    }
}
