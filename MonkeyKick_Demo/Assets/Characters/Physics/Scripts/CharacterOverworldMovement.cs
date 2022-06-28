// Merle Roji 6/28/22

using UnityEngine;

namespace MonkeyKick.Characters
{
    /// <summary>
    /// Controls character movement during the overworld portions
    /// 
    /// Notes:
    /// - make sure to add Game Manager
    /// - make sure to add Game State functionality
    /// </summary>

    public class CharacterOverworldMovement : MonoBehaviour
    {
        [SerializeField] protected Transform _cameraDirection;

        protected CharacterPhysics _physics;
        protected bool _isMoving;
        protected Vector2 _movement;
        public Vector2 CurrentMovement { get => _movement; }

        public virtual void Awake()
        {
            _physics = GetComponent<CharacterPhysics>();
        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {
            _physics?.ObeyGravity();
        }
    }
}
