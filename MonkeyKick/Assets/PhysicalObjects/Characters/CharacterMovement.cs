// Merle Roji
// 10/5/21

using UnityEngine;
using MonkeyKick.Managers;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public abstract class CharacterMovement : MonoBehaviour
    {
        public GameManager GameManager;

        private CharacterBattle _battle;

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
            if (GameManager.InOverworld())
            {
                _physics = GetComponent<IPhysics>();
                _battle = GetComponent<CharacterBattle>();
            }
        }

        public virtual void Update()
        {
            if (!GameManager.InOverworld())
            {
                if (GameManager.InBattle()) _battle.enabled = true;

                this.enabled = false;
            }
        }

        public virtual void FixedUpdate()
        {
            if (GameManager.InOverworld())
            {
                _physics?.ObeyGravity();
            }
        }

        public virtual void OnEnable()
        {
            if (!GameManager.InOverworld()) this.enabled = false;
        }

        #endregion
    }
}
