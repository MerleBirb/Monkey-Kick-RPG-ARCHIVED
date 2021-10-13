// Merle Roji
// 10/5/21

using UnityEngine;
using MonkeyKick.Managers;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Store the Game Manager for the Game State")]
        public GameManager GameManager;

        #region PHYSICS

        protected IPhysics _physics;
        protected Vector2 _movement;
        protected bool _isMoving;

        [SerializeField] protected Transform direction;

        public Vector2 CurrentVelocity { get { return _movement; } }

        #endregion

        #region UNITY METHODS

        public virtual void Awake()
        {
            _physics = GetComponent<IPhysics>();
        }

        public virtual void FixedUpdate()
        {
            _physics?.ObeyGravity();
        }

        #endregion
    }
}
