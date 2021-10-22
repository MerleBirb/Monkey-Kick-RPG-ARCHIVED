// Merle Roji
// 10/12/21

using System;
using System.Collections;
using UnityEngine;
using MonkeyKick.Managers;
using MonkeyKick.QualityOfLife;
using MonkeyKick.RPGSystem;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public enum BattleStates
    {
        EnterBattle,
        Wait,
        ChooseAction,
        Action
    }

    public abstract class CharacterBattle : MonoBehaviour
    {
        #region RPG BATTLE SYSTEM

        [SerializeField] protected GameManager gameManager;
        [HideInInspector] public TurnClass Turn;
        public CharacterStats Stats;
        protected TurnSystem _turnSystem;
        protected bool _isTurn;
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

        [Header("The position the character goes when entering combat")]
        [SerializeField] private Vector2 _startingBattlePos;
        protected Vector2 _battlePos;
        public Vector2 BattlePos { get => _battlePos; }

        #endregion

        #region ANIMATIONS

        protected Animator _anim;
        protected string _currentState = "";
        protected const string BATTLE_STANCE = "BattleStance";

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
            if (_isTurn != Turn.isTurn) { _isTurn = Turn.isTurn; }
        }

        private void FixedUpdate()
        {
            _physics?.ObeyGravity();
        }

        #endregion

        #region BATTLE STATES

        protected virtual void EnterBattle()
        {
            // set up battle position
            StartCoroutine(JumpIntoBattlePosition());

            // if turn system not injected
            if (!_turnSystem) _turnSystem = FindObjectOfType<TurnSystem>();

            // get dependencies for turn from turn order
            foreach(TurnClass tc in _turnSystem.TurnOrder)
            {
                if (tc.character.name == gameObject.name) Turn = tc;
            }
        }

        protected virtual void Wait()
        {
            if (_isTurn) _battleState = BattleStates.ChooseAction;
        }

        private IEnumerator JumpIntoBattlePosition()
        {
            Vector3 landPos = transform.position + new Vector3(_startingBattlePos.x, 0f, _startingBattlePos.y);

            // jump into position
            ParabolaData jumpData = PhysicsQoL.CalculateParabolaData(transform.position, landPos, 1f, 0f, Physics.gravity.y);
            PhysicsQoL.ParabolaMove(jumpData, _physics?.GetRigidbody());
            yield return new WaitForSeconds(jumpData.TimeToTarget);

            // stop movement after jump
            _physics?.ResetMovement();

            yield return null;
        }

        public void ResetAfterAction()
        {
            _isTurn = false;
            Turn.isTurn = _isTurn;
            Turn.wasTurnPrev = true;

            if (!_isTurn) _battleState = BattleStates.Wait;
        }

        #endregion
    }
}
