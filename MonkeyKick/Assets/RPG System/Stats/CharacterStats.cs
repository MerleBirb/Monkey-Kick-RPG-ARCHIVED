// Merle Roji
// 10/11/21

using UnityEngine;
using System.Collections.Generic;

namespace MonkeyKick.RPGSystem
{
    [CreateAssetMenu(fileName = "Stats", menuName = "RPGSystem/Stats/CharacterStats", order = 1)]
    public class CharacterStats : ScriptableObject
    {
        #region STAT VARIABLES

        [Header("Basic Information")]
        [SerializeField] protected string _characterName = "Name";
        public string CharacterName { get => _characterName; }
        [SerializeField] protected int _level = 1;
        public int Level { get => _level; }
        [TextArea(15, 20)]
        [SerializeField] protected string _description = "Description";
        public string Description { get => _description; }

        [Header("How much damage the character can take")]
        [SerializeField] protected int _maxHP = 1;
        public int MaxHP { get => _maxHP; }
        [SerializeField] protected int _currentHP = 1;
        public int CurrentHP { get => _currentHP; }

        [Header("How much Ki the character has")]

        [SerializeField] protected int _maxKi = 1;
        public int MaxKi { get => _maxKi; }
        [SerializeField] protected int _currentKi = 1;
        public int CurrentKi { get => _currentKi; }

        [Header("Physical Damage")]
        [SerializeField] protected int _muscle = 1;
        public int Muscle { get => _muscle; }

        [Header("Physical Defense and Status Damage")]
        [SerializeField] protected int _toughness = 1;
        public int Toughness { get => _toughness; }

        [Header("Ki Damage scaling")]
        [SerializeField] protected int _smarts = 1;
        public int Smarts { get => _smarts; }

        [Header("Ki Defense and Status Chance")]
        [SerializeField] protected int _willpower = 1;
        public int Willpower { get => _willpower; }

        [Header("Affects where character goes in Turn Order")]
        [SerializeField] protected int _speed = 1;
        public int Speed { get => _speed; }

        [Header("Critical Chance and Shop Price")]
        [SerializeField] protected int _swag = 1;
        public int Swag { get => _swag; }

        [Header("List of Skills the player can use.")]
        [SerializeField] protected List<Skill> _skillList;
        public List<Skill> SkillList { get => _skillList; }

        const int STAT_CLAMP = 99999;
        protected void OnValidate()
        {
            _level = Mathf.Clamp(Level, 1, 100);

            _maxHP = Mathf.Clamp(MaxHP, 1, STAT_CLAMP);
            _currentHP = Mathf.Clamp(CurrentHP, 0, _maxHP);

            _maxKi = Mathf.Clamp(MaxKi, 1, STAT_CLAMP);
            _currentKi = Mathf.Clamp(CurrentKi, 0, _maxKi);

            _muscle = Mathf.Clamp(Muscle, 1, STAT_CLAMP);
            _toughness = Mathf.Clamp(Toughness, 1, STAT_CLAMP);
            _smarts = Mathf.Clamp(Smarts, 1, STAT_CLAMP);
            _willpower = Mathf.Clamp(Willpower, 1, STAT_CLAMP);
            _speed = Mathf.Clamp(Speed, 1, STAT_CLAMP);
            _swag = Mathf.Clamp(Swag, 1, STAT_CLAMP);
        }

        #endregion

        #region STAT CHANGING METHODS

        // Damage Calc
        public virtual void Damage(int value)
        {
            bool damageHPBelowZero = (_currentHP - value) <= 0;

            // make sure the HP never goes below zero
            if (damageHPBelowZero) _currentHP = 0;
            else _currentHP -= (int)value;
        }

        #endregion
    }
}
