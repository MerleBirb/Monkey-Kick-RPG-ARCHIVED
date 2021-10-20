// Merle Roji
// 10/12/21

using System.Collections;
using UnityEngine;
using MonkeyKick.Managers;
using MonkeyKick.QualityOfLife;
using MonkeyKick.RPGSystem;

namespace MonkeyKick.PhysicalObjects.Characters
{
    public enum BattleState
    {
        EnterBattle,
        Wait
    }

    public abstract class CharacterBattle : MonoBehaviour
    {
        #region RPG BATTLE SYSTEM

        [SerializeField] protected GameManager gameManager;
        [HideInInspector] public TurnClass Turn;
        public CharacterStats Stats;
        protected TurnSystem _turnSystem;
        protected BattleState _battleState = BattleState.EnterBattle;

        #endregion

        #region PHYSICS

        private IPhysics _physics;

        [Header("The position the character goes when entering combat")]
        [SerializeField] private Vector2 _startingBattlePos;
        protected Vector2 _battlePos;
        public Vector2 StartingBattlePos { get => _startingBattlePos; }

        #endregion

        #region ANIMATIONS

        protected Animator _anim;
        protected string _currentState = "";
        protected const string BATTLE_STANCE = "BattleStance";

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            _physics = GetComponent<CharacterPhysics>();
            _anim = GetComponentInChildren<Animator>();
            this.enabled = false;
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

        private IEnumerator JumpIntoBattlePosition()
        {
            Vector3 landPos = transform.position + new Vector3(_startingBattlePos.x, 0f, _startingBattlePos.y);

            // jump into position
            ParabolaData jumpData = PhysicsQoL.CalculateParabolaData(transform.position, landPos, 1f, 0f, Physics.gravity.y);
            PhysicsQoL.ParabolaMove(jumpData, _physics?.GetRigidbody());
            yield return new WaitForSeconds(jumpData.TimeToTarget);

            // stop movement after jump
            _physics?.ResetMovement();

            // save battle position for returning from skills and counterattacks
            _battlePos.x = transform.position.x;
            _battlePos.y = transform.position.z;

            yield return null;
        }

        #endregion
    }
}
