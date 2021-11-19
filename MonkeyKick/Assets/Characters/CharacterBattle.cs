// Merle Roji
// 10/12/21

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonkeyKick.Managers;
using MonkeyKick.QualityOfLife;
using MonkeyKick.CustomPhysics;

namespace MonkeyKick.RPGSystem.Characters
{
    public enum BattleStates
    {
        EnterBattle,
        Wait,
        ChooseAction,
        Action,
        Counter,
        Dead
    }

    public enum BodyParts
    {
        Head,
        Torso,
        RightArm,
        LeftArm,
        RightLeg,
        LeftLeg
    }

    public abstract class CharacterBattle : MonoBehaviour
    {
        [Header("The Game Manager holds the game state.")]
        [SerializeField] protected GameManager gameManager;

        #region HITBOXES

        [Header("Hitboxes that represent limbs")]
        public List<Transform> Hitboxes;

        [HideInInspector] public bool isInterrupted; // have they been interrupted by an attack

        #endregion

        #region RPG BATTLE SYSTEM

        [HideInInspector] public TurnClass Turn;
        public CharacterStats Stats;
        protected TurnSystem _turnSystem;
        protected bool _isTurn = false;
        private bool _battleStarted = false;
        protected BattleStates _battleState = BattleStates.EnterBattle;
        public BattleStates BattleState
        {
            get { return _battleState; }
            set
            {
                Type t = value.GetType(); // check if type is of BattleStates
                if (t.Equals(typeof(BattleStates))) _battleState = value;
            }
        }

        #endregion

        #region PHYSICS

        protected IPhysics _physics;
        public IPhysics CharacterPhysics { get => _physics; }

        [Header("The position the character goes when entering combat")]
        [SerializeField] private Vector2 _startingBattlePos;
        protected Vector2 _battlePos;
        public Vector2 BattlePos { get => _battlePos; }

        #endregion

        #region ANIMATIONS

        protected Animator _anim;
        protected string _currentState = "";
        protected const string BATTLE_STANCE_R = "BattleStance_right";
        protected const string BATTLE_STANCE_L = "BattleStance_left";

        #endregion

        #region UNITY METHODS

        protected virtual void Awake()
        {
            _physics = GetComponent<CharacterPhysics>();
            _anim = GetComponentInChildren<Animator>();
            this.enabled = false;
        }

        protected virtual void Update()
        {
            CheckHealth();
            if (_isTurn != Turn.isTurn) { _isTurn = Turn.isTurn; }
        }

        protected virtual void FixedUpdate()
        {
            _physics?.ObeyGravity();
        }

        protected virtual void OnDisable()
        {
            _battleState = BattleStates.EnterBattle;
            _battleStarted = false;
        }

        #endregion

        #region BATTLE STATE METHODS

        /// <summary>
        /// Actions that occur while in the Enter Battle state.
        /// </summary>
        protected virtual void EnterBattle()
        {
            // if turn system not injected
            if (!_turnSystem)
            {
                _turnSystem = FindObjectOfType<TurnSystem>();
                return;
            }

            // get dependencies for turn from turn order
            foreach(TurnClass tc in _turnSystem.TurnOrder)
            {
                if (tc.character.name == gameObject.name) Turn = tc;
            }

            // set up battle position
            if (!_battleStarted)
            {
                // jump into position
                StartCoroutine(JumpIntoBattlePosition());
                _battleStarted = true;
            }
        }

        protected virtual void Wait()
        {
            if (_isTurn) _battleState = BattleStates.ChooseAction;
            if (_turnSystem.EnemyPartyDefeated()) { OnBattleEnd.Invoke(); }
        }

        private IEnumerator JumpIntoBattlePosition()
        {
            Vector3 landPos = transform.position + new Vector3(_startingBattlePos.x, 0f, _startingBattlePos.y);

            //jump into position
            ParabolaData jumpData = PhysicsQoL.CalculateParabolaData(transform.position, landPos, 1f, 0f, Physics.gravity.y);
            PhysicsQoL.ParabolaMove(jumpData, _physics?.GetRigidbody());

            yield return new WaitForSeconds(jumpData.TimeToTarget);

            if (_turnSystem.TurnSystemLoaded && _physics.OnGround())
            {
                // stop movement after jump
                _physics?.ResetMovement();
                _battleState = BattleStates.Wait;
            }

            yield return null;
        }

        public void ResetAfterAction()
        {
            _isTurn = false;
            Turn.isTurn = _isTurn;
            Turn.wasTurnPrev = true;

            if (!_isTurn) _battleState = BattleStates.Wait;
        }

        protected void CheckHealth()
        {
            if (Stats.CurrentHP <= 0)
            {
                _physics.ResetMovement();
                Turn.isDead = true;
                _battleState = BattleStates.Dead;
            }
        }

        #endregion

        #region EVENTS

        // Battle end event
        public delegate void BattleEndTrigger();
        public BattleEndTrigger OnBattleEnd;
        public void InvokeOnBattleEnd()
        {
            OnBattleEnd?.Invoke();
        }

        #endregion
    }
}
