// Merle Roji
// 10/5/21

using UnityEngine;
using MonkeyKick.Managers;
using MonkeyKick.CustomPhysics;

namespace MonkeyKick.RPGSystem.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("The Game Manager holds the game state.")]
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

        public virtual void Update()
        {
            if (gameManager.GameState != GameStates.Overworld) return;
        }

        public virtual void FixedUpdate()
        {
            if (gameManager.GameState != GameStates.Overworld) return;

            _physics?.ObeyGravity();
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
