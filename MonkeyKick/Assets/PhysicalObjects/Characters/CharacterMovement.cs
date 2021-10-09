// Merle Roji
// 10/5/21

using UnityEngine;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public abstract class CharacterMovement : MonoBehaviour
    {
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
