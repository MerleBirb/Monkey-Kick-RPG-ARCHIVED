//===== CHARACTER OVERWORLD =====//
/*
5/23/21
Description:
- Abstract class to hold general information for all character logic in the overworld game state.

Author: Merlebirb
*/

using UnityEngine;
using MonkeyKick.Managers;
using MonkeyKick.QoL;
using MonkeyKick.CameraTools;
using MonkeyKick.EntityInformation;

namespace MonkeyKick.Overworld
{
    public class CharacterOverworld : MonoBehaviour
    {
        [SerializeField] protected CharacterInformation Stats;
        [SerializeField] protected GameStateData Game;

        protected Vector2 _movement;
        [SerializeField] private Transform characterSpace;
        [SerializeField] protected float moveSpeed;

        protected IPhysics _physics;
        protected Rigidbody _rb;
        protected Animator _anim;
        protected string _currentAnim;
        protected Direction _directionState;
        protected Vector2 _directionVector;
        protected int _direction = 0;
        protected bool _isMoving = false;

        public virtual void Awake()
        {
            if (!Game.CompareGameState(GameStates.Overworld)) { this.enabled = false; }
            else
            {
                _rb = GetComponent<Rigidbody>();
                _physics = GetComponent<IPhysics>();
                _anim = GetComponentInChildren<Animator>();
            }
        }

        public virtual void Update()
        {
            if (!Game.CompareGameState(GameStates.Overworld)) { this.enabled = false; }

            _physics?.CheckIfGravityShouldApply(_rb);
            DetermineDirectionState();
        }

        public virtual void FixedUpdate()
        {
            if (_physics != null)
            {
                ApplyPhysics(moveSpeed);
            }
        }

        public void ApplyPhysics(float speed)
        {
            if (characterSpace) { _rb.velocity = _physics.Movement(_movement, speed, _rb.velocity.y, characterSpace); }
            else { _rb.velocity = _physics.Movement(_movement, speed, _rb.velocity.y); }

            _physics.UpdatePhysicsCount(SnapToGround());
            _physics.ClearPhysicsCount();

            _isMoving = Mathf.Abs(_movement.x) > 0 || Mathf.Abs(_movement.y) > 0;
        }

        public bool SnapToGround()
        {
            float _speed = _rb.velocity.magnitude;

            if (_physics.GetStepsSinceLastGrounded() > 1 || _physics.GetStepsSinceLastAerial() <= 3)
            {
                return false;
            }

            if (!Physics.Raycast(_rb.position, -Vector3.up, out RaycastHit hit, 1f, -1))
            {
                return false;
            }

            float _dot = Vector3.Dot(_rb.velocity, hit.normal);
            if (_dot > 0f)
            {
                _rb.velocity = (_rb.velocity - (hit.normal * _dot)).normalized * _speed;
            }

            return true;
        }

        private void DetermineDirectionState()
        {
            int dir = OrbitCamera.OrbitDirection - _direction;

            switch(dir)
            {
                case 0: { _directionState = Direction.South; break; }
                case -1: case 7: { _directionState = Direction.SouthWest; break; }
                case -2: case 6: { _directionState = Direction.West; break; }
                case -3: case 5: { _directionState = Direction.NorthWest; break; }
                case 4: case -4: { _directionState = Direction.North; break; }
                case 3: case -5: { _directionState = Direction.NorthEast; break; }
                case 2: case -6: { _directionState = Direction.East; break; }
                case 1: case -7: { _directionState = Direction.SouthEast; break; }
            }
        }
    }
}