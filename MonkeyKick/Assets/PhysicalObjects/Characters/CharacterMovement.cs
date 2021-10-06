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

        [SerializeField] protected Transform direction;
        [SerializeField] protected float moveSpeed = 1;

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
