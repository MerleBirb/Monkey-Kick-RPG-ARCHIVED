//===== CHARACTER OVERWORLD =====//
/*
5/23/21
Description:
- Abstract class to hold general information for all character logic in the overworld game state.

Author: Merlebirb
*/

using UnityEngine;
using MonkeyKick.Managers;

namespace MonkeyKick.Overworld
{
    public class CharacterOverworld : MonoBehaviour
    {
        [SerializeField] protected GameStateData Game;

        protected Vector2 movement;
        [SerializeField] private Transform characterSpace;
        [SerializeField] protected float moveSpeed;

        protected IPhysics physics;
        protected Rigidbody rb;
        protected Animator _anim;
        protected bool _isMoving = false;

        public virtual void Awake()
        {
            if (!Game.CompareGameState(GameStates.Overworld)) { this.enabled = false; }
            else
            {
                rb = GetComponent<Rigidbody>();
                physics = GetComponent<IPhysics>();
                _anim = GetComponentInChildren<Animator>();
            }
        }

        public virtual void Update()
        {
            if (!Game.CompareGameState(GameStates.Overworld)) { this.enabled = false; }

            physics?.CheckIfGravityShouldApply(rb);
        }

        public virtual void FixedUpdate()
        {
            if (physics != null)
            {
                ApplyPhysics(moveSpeed);
            }
        }

        public void ApplyPhysics(float _speed)
        {
            if (characterSpace) { rb.velocity = physics.Movement(movement, _speed, rb.velocity.y, characterSpace); }
            else { rb.velocity = physics.Movement(movement, _speed, rb.velocity.y); }

            physics.UpdatePhysicsCount(SnapToGround());
            physics.ClearPhysicsCount();

            _isMoving = Mathf.Abs(movement.x) > 0 || Mathf.Abs(movement.y) > 0;
        }

        public bool SnapToGround()
        {
            float _speed = rb.velocity.magnitude;

            if (physics.GetStepsSinceLastGrounded() > 1 || physics.GetStepsSinceLastAerial() <= 3)
            {
                return false;
            }

            if (!Physics.Raycast(rb.position, -Vector3.up, out RaycastHit hit, 1f, -1))
            {
                return false;
            }

            float _dot = Vector3.Dot(rb.velocity, hit.normal);
            if (_dot > 0f)
            {
                rb.velocity = (rb.velocity - (hit.normal * _dot)).normalized * _speed;
            }

            return true;
        }
    }
}