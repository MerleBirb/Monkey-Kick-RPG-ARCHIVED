// Merle Roji
// 10/5/21

using UnityEngine;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public abstract class CharacterMovement : MonoBehaviour
    {
        #region PHYSICS

        protected Rigidbody _rb;
        protected IPhysics _physics;
        protected Vector2 _movement;
        protected bool _isMoving;

        public Vector2 CurrentVelocity { get { return _movement; } }
        public bool IsMoving { get { return _isMoving; } }

        [SerializeField] protected Transform direction;
        [SerializeField] protected float moveSpeed = 1;

        public Transform CurrentDirection { get { return direction; } }

        #endregion

        #region UNITY METHODS

        public virtual void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _physics = GetComponent<IPhysics>();
        }

        #endregion
    }
}
