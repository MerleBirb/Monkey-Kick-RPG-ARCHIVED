// Merle Roji 7/12/22

using System;
using UnityEngine;
using MonkeyKick.Managers.TurnSystem;

namespace MonkeyKick.Characters
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

    /// <summary>
    /// Handles battle logic for characters.
    /// 
    /// Notes:
    /// 
    /// </summary>
    public abstract class CharacterBattle : MonoBehaviour
    {
        // turn
        protected Turn _turn;
        public Turn Turn
        {
            get => _turn;
            set => _turn = value;
        }

        protected bool _isTurn = false;

        // turn system
        protected CharacterInformation _stats;
        protected TurnManager _turnManager;
        protected bool _hasBattleStarted = false;
        protected BattleStates _battleState = BattleStates.EnterBattle;
        public BattleStates BattleState
        {
            get => _battleState;
            set
            {
                Type t = value.GetType(); // check if type is of BattleStates
                if (t.Equals(typeof(BattleStates))) _battleState = value;
            }
        }

        // character
        protected Vector2 _battlePos;
        public Vector2 BattlePos { get => _battlePos; }
        private Rigidbody _rigidbody;
        public Rigidbody Rigidbody { get => _rigidbody; }
        private CapsuleCollider _collider;
        public CapsuleCollider Collider { get => _collider; }
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _maxGroundAngle = 60f; // for slope angles
        private RaycastHit _hitGround; // hit of whatever is grounding the character
        private Vector3 _currentGravity; // new gravity
        private Vector3 _groundNormal; // normal of whatever is grounding the character

        protected virtual void Awake()
        {

        }

        protected virtual void Update()
        {
            CheckHealth();
            if (_isTurn != _turn.IsTurn) { _isTurn = _turn.IsTurn; }
        }

        protected virtual void FixedUpdate()
        {
            ObeyGravity();
        }

        protected virtual void OnDisable()
        {
            _battleState = BattleStates.EnterBattle;
            _hasBattleStarted = false;
        }

        protected virtual void EnterBattle()
        {
            if (!_hasBattleStarted)
            {
                // inject turn system
                if (!_turnManager)
                {
                    _turnManager = FindObjectOfType<TurnManager>();
                }

                // get dependencies for turn from turn order
                foreach (Turn t in _turnManager.TurnOrder)
                {
                    if (t.Character.name == gameObject.name) _turn = t;
                }

                // set battle position
                _battlePos.x = transform.position.x;
                _battlePos.y = transform.position.z;
                _battleState = BattleStates.Wait;

                _hasBattleStarted = true;
            }
        }

        protected virtual void Wait()
        {
            if (_isTurn) _battleState = BattleStates.ChooseAction;
        }

        public void ResetAfterAction()
        {
            _isTurn = false;
            _turn.IsTurn = _isTurn;
            _turn.WasTurnPrev = true;

            if (!_isTurn) _battleState = BattleStates.Wait;
        }

        protected void CheckHealth()
        {
            if (_stats.CurrentKi == 0)
            {
                ResetMovement();
                _turn.IsDead = true;
                _battleState = BattleStates.Dead;
            }
        }

        protected void ResetMovement()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        protected void TurnOffGravity()
        {
            _rigidbody.useGravity = false;
        }

        protected bool OnGround()
        {
            float adjustHeight = (_collider.height / 2f) + 0.1f;
            return Physics.Raycast(_rigidbody.position, -Vector3.up, out _hitGround, adjustHeight, _groundLayer); // raycast down, true if object is ground layer, store hit
        }

        protected void ObeyGravity()
        {
            if (_maxGroundAngle > Vector3.Angle(_hitGround.normal, -Physics.gravity.normalized)) _groundNormal = _hitGround.normal;

            if (!OnGround()) _currentGravity = Physics.gravity; // normal gravity, when not grounded
            else if (OnGround()) _currentGravity = -_groundNormal * Physics.gravity.magnitude; // gravity perpendicular to a slope

            _rigidbody.AddForce(_currentGravity, ForceMode.Acceleration);
        }
    }
}

