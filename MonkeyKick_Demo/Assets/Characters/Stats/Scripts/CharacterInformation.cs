// Merle Roji 7/9/22

using UnityEngine;

namespace MonkeyKick.Characters
{
    /// <summary>
    /// Holds information like battle stats, name, etc.
    /// 
    /// Notes:
    /// 
    /// 
    /// </summary>

    [CreateAssetMenu(fileName = "Character Info", menuName = "RPG/Character/Character Info")]
    public class CharacterInformation : ScriptableObject
    {
        [Header("Basic Information")]
        [SerializeField] protected string _characterName = "Name";
        public string CharacterName { get => _characterName; }

        [TextArea(15, 20)] [SerializeField] protected string _description;
        public string Description { get => _description; }

        [Header("Ki: Both the Health resource and Energy resource.")]
        [SerializeField] protected int _maxKi = 1;
        public int MaxKi { get => _maxKi; }
        [SerializeField] protected int _currentKi = 1;
        public int CurrentKi { get => _currentKi; }

        [Header("Attack: Affects damage dealt.")]
        [SerializeField] protected int _attack = 1;
        public int Attack { get => _attack; }

        [Header("Defense: Affects damage taken.")]
        [SerializeField] protected int _defense = 1;
        public int Defense { get => _defense; }

        [Header("Speed: Affects turn order.")]
        [SerializeField] protected int _speed = 1;
        public int Speed { get => _speed; }

        [Header("Swag: Affects Critical Hit Chance.")]
        [SerializeField] protected int _swag = 1;
        public int Swag { get => _swag; }

        const int STAT_CLAMP = 99999;
        protected void OnValidate()
        {
            _maxKi = Mathf.Clamp(_maxKi, 1, STAT_CLAMP);
            _currentKi = Mathf.Clamp(_currentKi, 0, _maxKi);

            _attack = Mathf.Clamp(_attack, 1, STAT_CLAMP);
            _defense = Mathf.Clamp(_defense, 1, STAT_CLAMP);
            _speed = Mathf.Clamp(_speed, 1, STAT_CLAMP);
            _swag = Mathf.Clamp(_swag, 1, STAT_CLAMP);
        }
    }
}
