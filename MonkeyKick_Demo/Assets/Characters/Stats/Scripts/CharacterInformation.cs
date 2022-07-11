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
    }
}
