// Merle Roji
// 10/5/21

using UnityEngine;
using MonkeyKick.Managers;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public abstract class CharacterMovement : MonoBehaviour
    {
        [SerializeField] protected GameManager gameManager;

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
            if (gameManager.GameState == GameStates.Overworld)
            {
                _physics = GetComponent<IPhysics>();
            }
            else
            {
                this.enabled = false;
            }
        }

        public virtual void FixedUpdate()
        {
            _physics?.ObeyGravity();
        }

        #endregion

        #region METHODS

        public enum CharacterTypes
        {
            Player,
            Enemy
        }

        #endregion

        #region EVENTS

        // Battle event
        public delegate void BattleStartTrigger();
        public event BattleStartTrigger OnBattleStart;
        public void InvokeOnBattleStart()
        {
            OnBattleStart?.Invoke();
        }

        #endregion
    }
}