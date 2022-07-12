// Merle Roji 7/11/22

using UnityEngine;

namespace MonkeyKick.Characters
{
    /// <summary>
    /// Handles information of characters whether they are in battle or not.
    /// 
    /// Notes:
    /// - make sure to keep decoupled from overworld and battle mechanics
    /// 
    /// </summary>
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected CharacterInformation _stats;

        protected CapsuleCollider _collider;
        public CapsuleCollider Collider { get => _collider; }

        public virtual void Awake()
        {
            _collider = GetComponent<CapsuleCollider>();
        }
    }
}

